using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SensorType
{
    public class SensorTypeCreateDTO
    {
        [SwaggerSchema("The sensor type name", Nullable = false)]
        public string TypeName { get; set; } = string.Empty;
        [SwaggerSchema("The sensor type measurement unit", Nullable = false)]
        public string Unit { get; set; } = string.Empty;
    }
}
