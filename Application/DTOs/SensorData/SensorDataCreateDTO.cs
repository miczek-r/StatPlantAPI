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
        [SwaggerSchema("The data owner device uuid", Nullable = false)]
        public string Id { get; set; }
        [SwaggerSchema("The sensor data")]
        public List<SensorDataSingleDTO> Data { get; set; } = new ();
        public string MacAddress { get; set; }
    }
}
