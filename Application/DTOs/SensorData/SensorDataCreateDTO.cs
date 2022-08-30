using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SensorData
{
    public class SensorDataCreateDTO
    {
        [SwaggerSchema("The data owner device identifier", Nullable = false)]
        public int DeviceId { get; set; }
        [SwaggerSchema("The sensor identifier", Nullable = false)]
        public int SensorId { get; set; }
        [SwaggerSchema("The value of measurement", Nullable = false)]
        public float Value { get; set; }
        [SwaggerSchema("The date of measurement", Nullable = false)]
        public DateTime DateOfMeasurement { get; set; }
    }
}
