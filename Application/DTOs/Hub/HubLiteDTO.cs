using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Hub
{
    public class HubLiteDTO
    {
        [SwaggerSchema("The hub identifier", Nullable = false)]
        public string Id { get; set; } = string.Empty;
        [SwaggerSchema("The hub name", Nullable = false)]
        public string Name { get; set; } = string.Empty;
        [SwaggerSchema("The hub description", Nullable = true)]
        public string? Description { get; set; }
        [SwaggerSchema("The hub mac address", Nullable = false)]
        public string MacAddress { get; set; } = String.Empty;
    }
}
