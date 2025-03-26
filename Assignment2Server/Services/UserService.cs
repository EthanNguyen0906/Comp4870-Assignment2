using Assignment2Library.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment2Server.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<List<User>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        
        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
        
        public async Task ApproveUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Approved = true;
                await _userManager.UpdateAsync(user);
            }
        }
        
        public async Task UnapproveUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Approved = false;
                await _userManager.UpdateAsync(user);
            }
        }
        
        public async Task PromoteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                // Set the role to "Admin"
                user.Role = "Admin";
                await _userManager.UpdateAsync(user);
            }
        }
        
        public async Task DemoteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                // Revert the role to "Contributor"
                user.Role = "Contributor";
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
