using Application.DTOs.Hub;
using Application.DTOs.SensorData;
using Application.DTOs.SensorType;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Device
{
    public class DeviceBaseDTO
    {
        [SwaggerSchema("The device identifier", Nullable = false)]
        public string Id { get; set; } = string.Empty;
        [SwaggerSchema("The device name", Nullable = false)]
        public string Name { get; set; } = string.Empty;
        [SwaggerSchema("The device description", Nullable = true)]
        public string? Description { get; set; }
        [SwaggerSchema("The device mac address", Nullable = false)]
        public string MacAddress { get; set; } = String.Empty;
        [SwaggerSchema("The devices hub")]
        public HubBaseDTO Hub { get; set; }
        [SwaggerSchema("List of sensor data")]
        public List<SensorDataBaseDTO> SensorData { get; set; } = new ();
    }
}
