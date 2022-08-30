using Application.DTOs.SensorType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ISensorTypeService
    {
        Task<IEnumerable<SensorTypeBaseDTO>> GetAll();
        Task<int> Create(SensorTypeCreateDTO sensorTypeCreateDTO);
        Task Remove(int id);
    }
}
