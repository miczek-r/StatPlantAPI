using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SensorData
{
    public class SensorDataGetDetailsDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DeviceId { get; set; }
        public int SensorType { get; set; }
    }
}
