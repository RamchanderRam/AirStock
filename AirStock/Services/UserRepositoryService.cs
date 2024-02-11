using System.Threading.Tasks;
using AirStock.Common.Models;
using AirStock.Models;
using AirStock.DAL;
using AirStock.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using AirStock.Services;

namespace AirStock.Services
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private readonly StockContext _dbContext;
        private readonly IServiceRoutine _sr;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserRepositoryService(StockContext dbContext, IConfiguration config, IServiceRoutine sr, IHttpContextAccessor contextAccessor)
        {
            _sr = sr;
            _config = config;
            _dbContext = dbContext;
            _contextAccessor = contextAccessor;
        }

        public async Task<UserModelAdapter> GetUserAsync(string username, string password)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == username && u.PasswordHash == password);
            UserModelAdapter userModelAdapter = new UserModelAdapter();
            var role = await _dbContext.Roles.Select(r=> r.Name).FirstOrDefaultAsync();

            if (user != null)
            {
                var userAdapter = new UserModelAdapter
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    PasswordHash = user.PasswordHash,
                    Email = user.Email,
                    RoleName = role,
                    // Copy other properties as needed...
                };

                return userAdapter;
            }
            else
            {
                return null; // Handle the case where the user is not found
            }
        }

        public async Task<List<string>> GetUserRoles(UserModelAdapter user)
        {
            var roles = await _dbContext.UserModels
                .Where(u => u.UserId == user.UserId)
                .Select(u => u.RoleName)
                .ToListAsync();
            return roles;
        }

        public async Task<List<UserModelAdapter>> GetUsers(UserModelAdapter userModelAdapter)
        {
            var query = _dbContext.UserModels.AsQueryable();

            if (!string.IsNullOrEmpty(userModelAdapter.RoleName))
            {
                query = query.Where(u => u.RoleName == userModelAdapter.RoleName);
            }

            var users = await query
                .Select(u => new UserModelAdapter
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    PasswordHash = u.Password,
                    Email = u.Email,
                    RoleName = u.RoleName,
                    // Copy other properties as needed...
                })
                .ToListAsync();

            return users;
        }

        public async Task<List<string>> GetUserRolesAsync(UserModelAdapter user)
        {
            // Implement the logic to fetch roles associated with the user.
            // This may involve querying your data store (e.g., a database) to retrieve the roles.
            // Return a list of role names.

            // Example: Fetch roles for a user with a specific user ID
            var roles = await _dbContext.UserModels
         .Where(ur => ur.UserId == user.UserId) // Replace with the actual property used for user identification
         .Select(ur => ur.RoleName)
         .ToListAsync();
            return roles;

        }


        public async Task<UserModelAdapter> GetUserByUsername(string username)
        {
            var user = await _dbContext.UserModels
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                return new UserModelAdapter
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    PasswordHash = user.Password,
                    Email = user.Email,
                    RoleName = user.RoleName,
                    // Copy other properties as needed...
                };
            }

            return null; // Handle the case when the user is not found
        }
    }
}
