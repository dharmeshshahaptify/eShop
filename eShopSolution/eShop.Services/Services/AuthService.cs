using eShop.Entities;
using eShop.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Services.Services
{
    public class AuthService : IAuthService
    {
        protected SignInManager<User> _signInManager;
        protected UserManager<User> _userManager;
        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<User> AuthenticateUser(string Username, string Password)
        {
            var result = await _signInManager.PasswordSignInAsync(Username, Password, false, false);
            if (result.Succeeded)
            {
                User user = await _userManager.FindByNameAsync(Username);
                var roles = await _userManager.GetRolesAsync(user);
                user.Roles = roles.ToArray();

                return user;
            }
            return null;
        }

        public async Task<bool> CreateUser(User user, string Password)
        {
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                //Admin, User
                string role = "User";
                var res = await _userManager.AddToRoleAsync(user, role);
                if (res.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
