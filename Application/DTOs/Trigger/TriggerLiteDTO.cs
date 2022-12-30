using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Trigger
{
    public class TriggerLiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Interval Interval { get; set; } 
        public int ConditionsCount { get; set; }
    }
}
