using Application.DTOs.Authentication;
using Application.Exceptions;
using Application.IServices;
using Core.Entities;
using Core.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService( UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task ConfirmEmail(EmailConfirmationDTO emailConfirmationDTO)
        {
            var userId = HttpUtility.UrlDecode(emailConfirmationDTO.UserId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new ObjectNotFoundException("User does not exists");
            }

            var token = HttpUtility.UrlDecode(emailConfirmationDTO.ConfirmationToken);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                string errors = "";
                //TODO: Rewrite to string builder
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                throw new ObjectValidationException(errors);
            }
        }

        public async Task<LoginResponseDTO> Login(LoginDTO model)
        {
            User? user = await _userManager.FindByNameAsync(model.Email);
            if (user is null)
            {
                throw new ObjectNotFoundException("User with this combination of email and password does not exist");
            }
            if (!user.EmailConfirmed)
            {
                throw new ObjectValidationException("User not activated. Confirm your email!");
            }
            SignInResult loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (loginResult.IsLockedOut)
            {
                throw new ObjectValidationException("User is locked out. Try again in 15 minutes");
            }
            if (loginResult.Succeeded)
            {
                TokenInfoDTO tokenInfo = await GenerateToken(user);
                return new LoginResponseDTO(tokenInfo);
            }
            throw new ObjectNotFoundException("User with this combination of email and password does not exist");
        }

        private async Task<TokenInfoDTO> GenerateToken(User user)
        {
            //TODO: Change loginProviderName
            await _userManager.RemoveAuthenticationTokenAsync(user, "loginProviderName", "JwtToken");
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            IList<Claim> claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), claims);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(31),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);
            await _userManager.SetAuthenticationTokenAsync(user, "loginProviderName", "JwtToken", jwtToken);

            return new TokenInfoDTO { Token = jwtToken, TokenValidUntil = tokenDescriptor.Expires.Value };
        }
    }
}
