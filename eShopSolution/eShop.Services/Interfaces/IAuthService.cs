using eShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> CreateUser(User user, string Password);
        Task<bool> SignOut();
        Task<User> AuthenticateUser(string Username, string Password);
    }
}
