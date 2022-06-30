using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserBaseDTO>> GetAll();
        Task<UserBaseDTO> GetById(string id);
        Task<string> Create(UserCreateDTO userCreateDTO);
    }
}
