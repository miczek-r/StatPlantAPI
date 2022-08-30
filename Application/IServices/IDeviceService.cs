using Application.DTOs.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceBaseDTO>> GetAll();
        Task<DeviceBaseDTO> GetById(int id);
        Task<int> Create(DeviceCreateDTO deviceCreateDTO);
        Task Remove(int id);
    }
}
