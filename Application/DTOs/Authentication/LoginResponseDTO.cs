using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Authentication
{
    public class LoginResponseDTO
    {
        [SwaggerSchema("Object that contains token info")]
        public TokenInfoDTO TokenInfo { get; set; }
        public LoginResponseDTO(TokenInfoDTO tokenInfo)
        {
            TokenInfo = tokenInfo;
        }
    }
}
