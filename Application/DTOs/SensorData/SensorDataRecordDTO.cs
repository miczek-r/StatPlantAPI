using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SensorData
{
    public class SensorDataRecordDTO
    {
        [SwaggerSchema("The value of measurement", Nullable = false)]
        public float MeasuredValue { get; set; }
        [SwaggerSchema("The date of measurement", Nullable = false)]
        public DateTime DateOfMeasurement { get; set; }
    }
}
