using AirStock.Models;
using X.PagedList;

namespace AirStock.Repositories
{
    public interface IVehicleService
    {
        Task<IPagedList<VehicleModel>> GetVehicleDetailsByFleetNumber(VehicleModel vehicleModel, int pageNumber, int pageSize, string searchTerm);
        List<VehicleModel> GetVehicleData();
        Task<bool> CreateVehicle(VehicleModel vehicle);
    }

}
