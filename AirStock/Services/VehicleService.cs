using model = AirStock.DAL;
using AirStock.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AirStock.Models;
using AirStock.Common.Models;
using AirStock.DAL;
//using PagedList;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using X.PagedList;
using Microsoft.AspNetCore.Mvc;

namespace AirStock.Services
{
    public class VehicleService : IVehicleService
    {
        readonly StockContext _dbContext;
        private IServiceRoutine _sr;
        private IConfiguration _config;
        //private readonly IUserRepository _userRepository;

        public VehicleService(StockContext dbContext, IConfiguration config, IServiceRoutine sr, IHttpContextAccessor contextAccessor)
        {
            _sr = sr;
            _config = config;
            _dbContext = dbContext;
        }

        public async Task<IPagedList<VehicleModel>> GetVehicleDetailsByFleetNumber(VehicleModel vehicleModel, int pageNumber, int pageSize, string searchTerm)
        {
            // Fetch vehicle details from the database based on the provided Fleet Number
            var vehicles = await _dbContext.Vehicles
                .Where(v => v.FleetNumber == vehicleModel.FleetNumber)
                .ToListAsync();

            if (vehicles != null && vehicles.Any())
            {
                // Filter by search term
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    vehicles = vehicles
                        .Where(v => v.FleetNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                // Map to VehicleDetails
                var vehicleDetailsList = vehicles.Select(v => new VehicleModel
                {
                    VehicleName = v.VehicleName,
                    VehicleNumber = v.VehicleNumber,
                    FleetNumber = v.FleetNumber,
                    Division = v.Division,
                    DateOfLastService = v.DateOfLastService // Corrected line, removed extra equals sign
                }).ToList();

                var pagedList = vehicleDetailsList.ToPagedList(pageNumber, pageSize); // Adjust accordingly

                return pagedList;
            }

            return null; // Return null if no vehicles found for the provided Fleet Number
        }

        public List<VehicleModel> GetVehicleData()
        {
            var vehicles = _dbContext.Vehicles.ToList();

            // Assuming you have a mapping or conversion method to convert your Entity to Model
            var vehicleModels = vehicles.Select(entity => GetServiceVehicle(entity)).ToList();

            return vehicleModels;
        }


        public async Task<bool> CreateVehicle(VehicleModel vehicle)
        {
            var entityClient = GetEntityVehicle(vehicle);
            //entityClient.CreatedOn = DateRoutine.GetCurrentDate();
            _dbContext.Vehicles.Add(entityClient);
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.InnerException?.ToString()); }

        }

        private Vehicle GetEntityVehicle(VehicleModel vehicleModel)
        {
            var entity = new Vehicle()
            {
                VehicleName = vehicleModel.VehicleName,
                VehicleNumber = vehicleModel.VehicleNumber,
                FleetNumber = vehicleModel.FleetNumber,
                Division = vehicleModel.Division,
                DriverKm = vehicleModel.DriverKm,
                DateOfLastService =vehicleModel.DateOfLastService
            };

            return entity;

        }


        private VehicleModel GetServiceVehicle(Vehicle vehicleEntity)
        {
            // Map properties from Vehicle entity to VehicleModel
            var model = new VehicleModel()
            {
                VehicleId = vehicleEntity.VehicleId,
                VehicleName = vehicleEntity.VehicleName,
                VehicleNumber = vehicleEntity.VehicleNumber,
                FleetNumber = vehicleEntity.FleetNumber,
                Division = vehicleEntity.Division,
                DriverKm = vehicleEntity.DriverKm,
                DateOfLastService = vehicleEntity.DateOfLastService
            };

            return model;
        }
    }

}