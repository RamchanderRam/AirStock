using System.Threading.Tasks;
using AirStock.Common.Models;
using AirStock.DAL;
using AirStock.Models;


namespace AirStock.Repositories
{
    public interface IUserRegistrationService
    {
        //Task<UserModelAdapter> GetUserByIdAsync(int userId);
        //Task<UserModelAdapter> GetUserByUsernameAsync(string username);
        //Task CreateUserAsync(UserModelAdapter user);
        //Task UpdateUserAsync(UserModelAdapter user);
        //Task DeleteUserAsync(int userId);
        Task<bool> RegisterUserAsync(UserModelAdapter userModel);

        // Define the GetUserProfile method
        Task<UserModelAdapter> GetUserProfile(string username);

    }
}
