using Application.DTOs.Sensor;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ISensorService
    {
        Task<IEnumerable<SensorBaseDTO>> GetAll();
        Task<SensorBaseDTO> GetById(int id);
        Task<int> Create(SensorCreateDTO sensorCreateDTO);
        Task Remove(int id);
    }
}
