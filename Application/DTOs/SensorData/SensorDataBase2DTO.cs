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
    public class SensorDataBase2DTO
    {
        [SwaggerSchema("The value of measurement", Nullable = false)]
        public float AverageValue { get; set; }
        [SwaggerSchema("The value of measurement", Nullable = false)]
        public string SensorTypeName { get; set; }
        [SwaggerSchema("The date of measurement", Nullable = false)]
        public int DateOfMeasurement { get; set; }

    }
}
