using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SensorType
{
    public class SensorTypeBaseDTO
    {
        [SwaggerSchema("The sensor type identifier", Nullable = false)]
        public int Id { get; set; }

        [SwaggerSchema("The sensor type name", Nullable = false)]
        public string TypeName { get; set; } = string.Empty;
        [SwaggerSchema("The sensor type measurement unit", Nullable = false)]
        public string Unit { get; set; } = string.Empty;
    }
}
