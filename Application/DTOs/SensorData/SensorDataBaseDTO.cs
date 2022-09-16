using Application.DTOs.Device;
using Application.DTOs.Sensor;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SensorData
{
    public class SensorDataBaseDTO
    {
        [SwaggerSchema("The sensor", Nullable = false)]
        public SensorBaseDTO Sensor { get; set; }
        [SwaggerSchema("The value of measurement", Nullable = false)]
        public float Value { get; set; }
        [SwaggerSchema("The date of measurement", Nullable = false)]
        public DateTime DateOfMeasurement { get; set; }

    }
}
