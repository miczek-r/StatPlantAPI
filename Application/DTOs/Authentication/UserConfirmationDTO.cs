using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Authentication
{
    public class EmailConfirmationDTO
    {
        [SwaggerSchema("The uri encoded user identificator", Nullable = false)]
        public string UserId { get; set; } = string.Empty;
        [SwaggerSchema("The uri encoded account confirmation token", Nullable = false)]
        public string? ConfirmationToken { get; set; } = string.Empty;
    }
}
