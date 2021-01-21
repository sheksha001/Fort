using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fort.Models
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user);
        Task<ServiceResponse<string>> Login(string username, string password);
    }
}
