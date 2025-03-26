using Assignment2Library.Data.Models;

namespace Assignment2Server.Services
{
    public interface IUserService
    {
        Task<List<Assignment2Library.Data.Models.User>> GetUsersAsync();
        Task DeleteUserAsync(string id);

        Task ApproveUserAsync(string id);

        Task UnapproveUserAsync(string id);
        Task PromoteUserAsync(string id);

        Task DemoteUserAsync(string id);

        Task<User?> GetUserByEmailAsync(string email);
    }
}
