using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Authentication
{
    public class TokenInfoDTO
    {
        [SwaggerSchema("The JWT token", Nullable = false)]
        public string Token { get; set; } = string.Empty;
        [SwaggerSchema("The date when JWT token expires", Format = "date-time")]
        public DateTime TokenValidUntil { get; set; }
    }
}
