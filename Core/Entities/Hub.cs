using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Hub : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MacAddress { get; set; }
        public string Password { get; set; }
        public List<Device> Devices { get; set; }
        public List<User> Users { get; set; }
    }
}
