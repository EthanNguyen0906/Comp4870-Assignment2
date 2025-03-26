using Assignment2Library.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Assignment2Server.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ConcurrentDictionary<string, SemaphoreSlim> _userLocks = new();

        public UserService(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userManager.Users
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task DeleteUserAsync(string id)
        {
            await ExecuteUserOperation(id, async user =>
            {
                await _userManager.DeleteAsync(user);
            });
        }

        public async Task ApproveUserAsync(string id)
        {
            await ExecuteUserOperation(id, async user =>
            {
                user.Approved = true;
                await _userManager.UpdateAsync(user);
            });
        }

        public async Task UnapproveUserAsync(string id)
        {
            await ExecuteUserOperation(id, async user =>
            {
                user.Approved = false;
                await _userManager.UpdateAsync(user);
            });
        }

        public async Task PromoteUserAsync(string id)
        {
            await ExecuteUserOperation(id, async user =>
            {
                user.Role = "Admin";
                await _userManager.UpdateAsync(user);
            });
        }

        public async Task DemoteUserAsync(string id)
        {
            await ExecuteUserOperation(id, async user =>
            {
                user.Role = "Contributor";
                await _userManager.UpdateAsync(user);
            });
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            // Use AsNoTracking for read-only operations
            return await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        private async Task ExecuteUserOperation(string userId, Func<User, Task> operation)
        {
            var semaphore = _userLocks.GetOrAdd(userId, _ => new SemaphoreSlim(1, 1));
            
            try
            {
                await semaphore.WaitAsync();
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await operation(user);
                }
            }
            finally
            {
                semaphore.Release();
                // Cleanup unused semaphores to prevent memory leaks
                if (semaphore.CurrentCount == 1)
                {
                    _userLocks.TryRemove(userId, out _);
                }
            }
        }
    }
}