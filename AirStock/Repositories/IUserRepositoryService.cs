using System.Collections.Generic;
using System.Threading.Tasks;
using AirStock.Common.Models;
using AirStock.DAL;
using AirStock.Models;

namespace AirStock.Repositories
{
    public interface IUserRepositoryService
    {
        Task<UserModelAdapter> GetUserAsync(string username, string password);
        Task<List<string>> GetUserRolesAsync(UserModelAdapter user);

        Task<List<UserModelAdapter>> GetUsers(UserModelAdapter user);

        Task<UserModelAdapter> GetUserByUsername(string username);
    }
}
