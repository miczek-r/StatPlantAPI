using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Sensor
{
    public class SensorCreateDTO
    {
        [SwaggerSchema("The sensor name", Nullable = false)]
        public string SensorName { get; set; } = string.Empty;
        [SwaggerSchema("The sensor type identifier", Nullable = false)]
        public int SensorTypeId { get; set; }
    }
}
