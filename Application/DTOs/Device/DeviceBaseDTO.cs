using Application.DTOs.Hub;
using Application.DTOs.SensorData;
using Application.DTOs.SensorType;
using Application.DTOs.Trigger;
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
        [SwaggerSchema("The device UUID", Nullable = false)]
        public string UUID { get; set; } = String.Empty;
        [SwaggerSchema("List of sensor data")]
        public List<SensorDataLiteDTO> SensorData { get; set; } = new ();
        [SwaggerSchema("List of detailed sensor data of past week")]
        public List<SensorDataBase2DTO> SensorDataDetails { get; set; } = new();
        public List<TriggerLiteDTO> CalledTriggers { get; set; } = new();
    }
}
