using Application.DTOs.User;
using Application.Exceptions;
using Application.IServices;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<string> Create(UserCreateDTO userCreateDTO)
        {
            var userExists = await _userManager.FindByNameAsync(userCreateDTO.Email);
            if(userExists is not null)
            {
                throw new ObjectAlreadyExistsException("User with this email already exists");
            }

            //TODO: Change to automapper
            User user = new()
            {
                Email = userCreateDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userCreateDTO.Email,
                FirstName = userCreateDTO.FirstName,
                LastName = userCreateDTO.LastName
            };

            var result = await _userManager.CreateAsync(user, userCreateDTO.Password);
            if (!result.Succeeded)
            {
                //TODO: Change to string builder
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                throw new ObjectValidationException(errors);
            }

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedEcToken = HttpUtility.UrlEncode(emailConfirmationToken);
            var encodedId = HttpUtility.UrlEncode(user.Id);
            //TODO: Add email sending service

            return user.Id;
        }

        public async Task<IEnumerable<UserBaseDTO>> GetAll()
        {
            IEnumerable<User> users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserBaseDTO>>(users);
        }

        public async Task<UserBaseDTO> GetById(string id)
        {
            User? user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(user is null)
            {
                throw new ObjectNotFoundException("User does not exists");
            }

            return _mapper.Map<UserBaseDTO>(user);
        }
    }
}
