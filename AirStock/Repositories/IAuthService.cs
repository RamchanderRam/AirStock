using AirStock.Common.Models;
using AirStock.DAL;
using AirStock.Models;


namespace AirStock.Repositories
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(UserModelAdapter userModel);

    }
}
