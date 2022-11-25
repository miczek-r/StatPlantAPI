using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SensorData
{
    public class SensorDataLiteDTO
    {
        public string SensorTypeName { get; set; }
        public int SensorTypeId { get; set; }
        public float Value { get; set; }
        public DateTime DateOfMeasurement { get; set; }
    }
}
