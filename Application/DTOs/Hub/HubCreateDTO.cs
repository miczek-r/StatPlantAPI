using Application.DTOs.Device;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Hub
{
    public class HubCreateDTO
    {
        [SwaggerSchema("The hub name", Nullable = false)]
        public string Name { get; set; } = string.Empty;
        [SwaggerSchema("The hub description", Nullable = true)]
        public string? Description { get; set; }
        [SwaggerSchema("The hub mac address", Nullable = false)]
        public string MacAddress { get; set; } = String.Empty;
        [SwaggerSchema("The list of hubs devices")]
        public List<DeviceCreateDTO> Devices { get; set; } = new();
        [SwaggerSchema("The list of hubs users identifiers")]
        public List<string> UserIds { get; set; } = new();
    }
}
