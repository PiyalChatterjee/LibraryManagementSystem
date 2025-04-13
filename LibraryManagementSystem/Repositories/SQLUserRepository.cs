using LMS.API.Data;
using LMS.API.Models.Domain;
using LMS.API.Models.DTOs;
using LMS.API.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly LMSDbContext dbContext;

        public SQLUserRepository(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<User> CreateUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var userEntity = dbContext.Users.Find(id);
            
            if (userEntity == null)
            {
                return false;
            }
            dbContext.Users.Remove(userEntity);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<User>> GetAllAsync()
        {
            
            return await dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            // Using Find method because it is optimized for primary key lookups and 
            // it can return the entity from the context cache if it was previously loaded.
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<User?> UpdateUserAsync(Guid id, User user)
        {
            var userEntity = await dbContext.Users.FindAsync(id);
            if (userEntity == null)
            {
                return null;
            }
            // Data mapping section
            userEntity.Username = user.Username;
            userEntity.Email = user.Email;
            userEntity.Password = user.Password ?? userEntity.Password; // In a real application, you should hash the password
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.Role = user.Role;
            userEntity.Status = user.Status;
            userEntity.DateCreated = userEntity.DateCreated; // Keep the original date created
            userEntity.LastLogin = DateTime.UtcNow; // Update last login time
            // Data update section
            await dbContext.SaveChangesAsync();
            return userEntity;
        }
    }
}
