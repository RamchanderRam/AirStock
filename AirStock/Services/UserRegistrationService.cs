using System.Threading.Tasks;
using AirStock.Common.Models;
using AirStock.Models;
using AirStock.DAL;
using AirStock.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AirStock.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        readonly StockContext _dbContext;
        private IServiceRoutine _sr;
        private IConfiguration _config;
        public UserRegistrationService(StockContext dbContext, IConfiguration config, IServiceRoutine sr, IHttpContextAccessor contextAccessor)
        {
            _sr = sr;
            _config = config;
            _dbContext = dbContext;
        }

        public async Task<bool> RegisterUserAsync(UserModelAdapter userModel)
        {
            // You can add validation logic here (e.g., check if the username is unique).
            // If validation fails, return null or throw an exception.
            UserModelAdapter userModelAdapter = new UserModelAdapter();
            var role = await _dbContext.Roles.Select(r => r.Name).FirstOrDefaultAsync();
            
            var user = new ApplicationUser
            {
                UserName = userModel.Username,
                PasswordHash = userModel.PasswordHash,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                EmailConfirmed = userModel.EmailConfirmed,
                PhoneNumber = userModel.PhoneNumber,
                PhoneNumberConfirmed = userModel.PhoneNumberConfirmed,
                AccessFailedCount = userModel.AccessFailedCount,
                NormalizedUserName = userModel.Username,
                NormalizedEmail = userModel.Email,
                //UserRoleId = userModel.UserRoleId,
                //role = userModel.RoleName
            };

            // Save the user to the database and retrieve the user's ID
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Now, create a UserModelAdapter from the UserModel
            var userAdapter = new UserModelAdapter
            {
                UserId = user.Id,
                Username = user.UserName,
                PasswordHash = user.PasswordHash, // Note: You should not store passwords as plain text
                Email = user.Email,
                //UserRoleId= user.UserRoleId,
                RoleName = userModel.RoleName
            };

            //return userAdapter;
            return true;
        }


        public async Task<UserModelAdapter> GetUserProfile(string username)
        {
            // Implement the logic to retrieve the user's profile based on the provided username.
            // For example, query the database to fetch the user's profile information.

            // Replace the following line with your actual database query logic
            var userProfile = await _dbContext.UserModels.FirstOrDefaultAsync(u => u.Username == username);

            if (userProfile != null)
            {
                // Create a UserModelAdapter instance from the retrieved data
                var userAdapter = new UserModelAdapter
                {
                    UserId = userProfile.UserId,
                    Username = userProfile.Username,
                    PasswordHash = userProfile.Password, // Modify as needed
                    Email = userProfile.Email,
                    RoleName = userProfile.RoleName
                };

                return userAdapter;
            }

            return null; // Handle the case where the user profile is not found.
        }



        //public async Task<UserModelAdapter> GetUserProfile(string username)
        //{
        //    // Implement logic to retrieve the user's profile based on the provided username
        //    // For example, query the database to fetch the user's profile information
        //    UserModelAdapter userProfile = await UserModel.FirstOrDefaultAsync(u => u.Username == username);

        //    return userProfile;
        //}


    }
}
