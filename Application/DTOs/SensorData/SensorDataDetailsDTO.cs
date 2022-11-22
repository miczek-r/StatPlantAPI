using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SensorData
{
    public class SensorDataDetailsDTO
    {
        [SwaggerSchema("The list of measurements", Nullable = false)]
        public List<SensorDataRecordDTO> SensorDataRecords { get; set; } = new();
        [SwaggerSchema("The typ")]
        public string TypeOfSensor { get; set; }
        public string DeviceInformations { get; set; }
        public string MeasurementUnit { get; set; }
        public SensorDataRecordDTO LatestRecordedValue { get; set; }
    }
}
