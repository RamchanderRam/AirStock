using AirStock.Models;

namespace AirStock.Repositories
{
    public interface IAdminDashboardService
    {
        Task<AdminDashboardModel> GetAdminDashboardDataAsync();
    }
}
