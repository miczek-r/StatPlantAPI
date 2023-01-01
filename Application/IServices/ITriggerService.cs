using Application.DTOs.Trigger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ITriggerService
    {
        Task<IEnumerable<TriggerLiteDTO>> GetAll(int deviceId);
        Task<TriggerBaseDTO> Get(int triggerId);
        Task<int> Create(TriggerCreateDTO triggerCreateDTO);
        Task Update(TriggerUpdateDTO triggerUpdateDTO);
        Task Remove(int triggerId);
        Task CheckAllTriggers();
    }
}
