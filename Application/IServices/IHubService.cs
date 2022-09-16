using Application.DTOs.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IHubService
    {
        Task<IEnumerable<HubLiteDTO>> GetAll();
        Task<HubBaseDTO> GetById(int id);
        Task<int> Create(HubCreateDTO hubCreateDTO);
        Task Remove(int id);
    }
}
