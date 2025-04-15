using LMS.API.Models.Domain;

namespace LMS.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(Guid id, User user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
