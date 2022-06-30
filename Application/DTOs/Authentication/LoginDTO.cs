using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Authentication
{
    public class LoginDTO
    {
        [SwaggerSchema("The user email", Nullable = false)]
        public string Email { get; set; } = string.Empty;
        [SwaggerSchema("The user password", Nullable = false, Format = "password")]
        public string Password { get; set; } = string.Empty;
    }
}
