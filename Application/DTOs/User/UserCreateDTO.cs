using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserCreateDTO
    {
        [SwaggerSchema("The user email", Nullable = false)]
        public string Email { get; set; } = string.Empty;
        [SwaggerSchema("The user password", Nullable = false, Format = "password")]
        public string Password { get; set; } = string.Empty;
        [SwaggerSchema("The user first name")]
        public string? FirstName { get; set; }
        [SwaggerSchema("The user last name")]
        public string? LastName { get; set; }
    }
}
