using Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDTO> Login(LoginDTO model);
        Task ConfirmEmail(EmailConfirmationDTO emailConfirmationDTO);
    }
}
