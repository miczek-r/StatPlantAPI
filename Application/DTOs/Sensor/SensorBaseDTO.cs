using Application.DTOs.SensorType;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Sensor
{
    public class SensorBaseDTO
    {
        [SwaggerSchema("The sensor name", Nullable = false)]
        public string SensorName { get; set; } = string.Empty;
        [SwaggerSchema("The sensor type")]
        public SensorTypeBaseDTO SensorType { get; set; }
    }
}
