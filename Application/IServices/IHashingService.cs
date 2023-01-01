using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IHashingService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hash);
    }
}
