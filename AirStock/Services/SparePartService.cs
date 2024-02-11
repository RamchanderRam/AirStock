using model = AirStock.DAL;
using AirStock.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AirStock.Models;
using AirStock.Common.Models;
using AirStock.DAL;
using PagedList;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using IdentityModel;

namespace AirStock.Services
{
    public class SparePartService : ISparePartService
    {
        private readonly StockContext _dbContext;

        public SparePartService(StockContext dbContext)
        {
            _dbContext = dbContext;
        }

        private SparePart GetEntitySparePart(SparePartModel sparePartModel)
        {
            var entity = new SparePart()
            {
                SparePartId = sparePartModel.SparePartId,
                Name = sparePartModel.Name,
                Quantity = sparePartModel.Quantity,
                SparePartCost = sparePartModel.SparePartCost,
                SparePartAmount = sparePartModel.SparePartAmount,
                JobCardId = sparePartModel.JobCardId,

            };

            return entity;
        }

        public static SparePartModel GetServiceSparePart(SparePart entity)
        {
            var jobCardModel = new SparePartModel()
            {
                SparePartId = entity.SparePartId,
                Name = entity.Name,
                Quantity = entity.Quantity,
                SparePartCost = entity.SparePartCost,
                SparePartAmount = entity.SparePartAmount,
                JobCardId = entity.JobCardId,
            };

            return jobCardModel;
        }

        public async Task<IPagedList<SparePartModel>> GetSpareParts(SparePartModel productAdapter, int pageNumber, int pageSize, string searchTerm)
        {

            var stockList = new List<SparePartModel>();

            var spareParts = await _dbContext.SpareParts.ToListAsync();
            if (!spareParts?.Any() ?? true)
            {
                throw new KeyNotFoundException("No records found");
            }
            if (!string.IsNullOrEmpty(searchTerm))
            {
                spareParts = spareParts.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!spareParts?.Any() ?? true)
            {
                throw new KeyNotFoundException("No records found");
            }
            var products = spareParts.Select(s => GetServiceSparePart(s)).ToPagedList(pageNumber, pageSize);
            return products;
        }

        public async Task<SparePartModel> GetByIdAsync(int id)
        {
            // Implement your logic to get a job card by ID
            var entity = await _dbContext.SpareParts.FindAsync(id);
            return entity != null ? GetServiceSparePart(entity) : null;
        }

        public async Task<bool> CreateSparePart(SparePartModel model)
        {
            try
            {
                var jobCard = await _dbContext.JobCards
                    .FirstOrDefaultAsync(j => j.JobCardId == model.JobCardId);

                if (jobCard == null)
                {
                    throw new InvalidOperationException("Invalid JobCardId");
                }

                var dbSparePart = new SparePart()
                {
                    Name = model.Name,
                    Quantity = model.Quantity,
                    SparePartCost = model.SparePartCost,
                    SparePartAmount = model.SparePartAmount,
                    JobCardId = model.JobCardId
                };

                _dbContext.SpareParts.Add(dbSparePart);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error creating SparePart: {ex.Message}");
                throw;
            }
        }








        public async Task<bool> UpdateSparePart(SparePartModel entity)
        {
            // Implement your logic to update a job card
            var dbJobCard = await _dbContext.SpareParts.FindAsync(entity.SparePartId);
            if (dbJobCard != null)
            {
                var entityJobCard = GetEntitySparePart(entity);
                entityJobCard.SparePartId = dbJobCard.SparePartId;
                _dbContext.Entry(dbJobCard).CurrentValues.SetValues(entityJobCard);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> RemoveSparePart(SparePartModel entity)
        {
            // Implement your logic to remove a job card
            var dbJobCard = await _dbContext.SpareParts.FindAsync(entity.SparePartId);
            if (dbJobCard != null)
            {
                _dbContext.SpareParts.Remove(dbJobCard);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }

}
