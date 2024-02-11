using AirStock.Models;
using AirStock.DAL;
using AirStock.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;
using AirStock.Common.Models;
#nullable disable


namespace AirStock.Services
{
    // public class StockService
    //[Route("api/[controller]")]

    public class StockService : IStockService
    {
        readonly StockContext _dbContext;
        //private IServiceRoutine _sr;
        //private IConfiguration _config;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IStockService _stockService;

        public StockService(StockContext dbContext /*IConfiguration config, IHttpContextAccessor httpContextAccessor, IServiceRoutine sr, IStockService stockService*/)
        {
            //_sr = sr;
            //_config = config;

            _dbContext = dbContext;
            //_httpContextAccessor = httpContextAccessor;
            //_stockService = stockService;

        }
        private Stock GetEntityStock(StockAdapter stockAdapter)
        {
            var entity = new Stock()
            {
                StockId = stockAdapter.StockId,
                ProductId = stockAdapter.ProductId,
                ProductName = stockAdapter.ProductName,
                ProductQuantity = stockAdapter.ProductQuantity,  
                ProductDate = stockAdapter.ProductDate,
                IsActive = stockAdapter.IsActive,
                //CreatedBy = modificationrequestadapter.CurrentUserLoginId,
                //CreatedOn = modificationrequestadapter?.CreatedOn == null || modificationrequestadapter.CreatedOn == DateTime.MinValue ? DateRoutine.GetCurrentDate() : modificationrequestadapter.CreatedOn,
                //UpdatedBy = modificationrequestadapter?.Id == 0 ? null : modificationrequestadapter?.CurrentUserLoginId,
                //UpdatedOn = modificationrequestadapter?.Id == 0 ? null : DateRoutine.GetCurrentDate()
            };
            //if (entity.StockId == 0)
            //{
            //    entity.StockId = _dbContext.Stocks.Where(x => x.StockId == stockAdapter.StockId).Select(s => s.StockId).FirstOrDefault();
            //}
            return entity;

        }
        private StockAdapter GetServiceStock(Stock entity)
        {
            var stockAdapter = new StockAdapter()
          
            {
                StockId = entity.StockId,
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                ProductQuantity = entity.ProductQuantity,
                ProductDate = (DateTime)(entity.ProductDate),
              
                IsActive = entity.IsActive,
              
                //CreatedBy = entity.CreatedBy,
                //CreatedOn = entity.CreatedOn,
                //UpdatedBy = entity?.UpdatedBy,
                //UpdatedOn = entity?.UpdatedOn
            };

            return stockAdapter;

        }

        public async Task<bool> AddStock(StockAdapter stockAdapter)
        {
            var dbStock = GetEntityStock(stockAdapter);
            _dbContext.Stocks.Add(dbStock);
            try
            {
                await _dbContext.SaveChangesAsync();

                //#region Email for Modification Request and Cancellation Request
                //var roles = $"{GroupConstant.MasterScheduler},{GroupConstant.Scheduler}";
                //var usersnotify = await _appnotification.GetUserbyGroupNames(roles);

                //var emails = string.Join(",", usersnotify?.Select(s => s.Email).ToList());
                //if (!string.IsNullOrEmpty(emails))
                //{
                //    string template = modificationrequestadapter.Type == "Cancellation" ? EmailTemplateConstants.APPOINTMENTCANCELLATIONREQUEST : EmailTemplateConstants.APPOINTMENTCHANGEREQUEST;
                //    await _appnotification.SendEmailNotification(template, EntityConstants.APPOINTMENTCHANGEREQUEST, dbmodificationrequest.AppointmentId.ToString(), emails, JsonConvert.SerializeObject(dbmodificationrequest));
                //}
                //#endregion
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.InnerException?.ToString()); }
        }
        public async Task<bool> UpdateStock(StockAdapter stockAdapter)
        {
            var dbStock = _dbContext.Stocks.Find(stockAdapter.StockId);
            if (dbStock == null)
            {
                throw new ApplicationException($"No record found with the id ({stockAdapter.StockId})");
            }
            //if (_dbContext.ModificationRequests.Where(t => (t.Type == modificationrequestadapter.Type) && t.ModificationRequestId != dbmodificationrequest.ModificationRequestId).Any())
          
            var entityStockAdapter = GetEntityStock(stockAdapter);
            entityStockAdapter.StockId = dbStock.StockId;
            //entityStockAdapter.CreatedBy = dbStock.CreatedBy;
            //entityStockAdapter.CreatedOn = dbStock.CreatedOn;
            _dbContext.Entry(dbStock).CurrentValues.SetValues(entityStockAdapter);
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.InnerException?.ToString()); }

        }
        public async Task<bool> DeleteStock(StockAdapter stockAdapter)
        {
            var dbStock = _dbContext.Stocks.Where(s => stockAdapter.StockId == s.StockId).FirstOrDefault();
            if (dbStock is null)
            {
                throw new KeyNotFoundException($"No record found with the id ({stockAdapter.StockId})");
            }
            _dbContext.Stocks.Remove(dbStock);
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.ToString());
            }
        }

        public async Task<List<StockAdapter>>  GetStock(StockAdapter stockAdapter)
        {
            var stockList = new List<ProductAdapter>();

            var dbStock = await _dbContext.Stocks.ToListAsync();
            if (!dbStock?.Any() ?? true)
            {
                throw new KeyNotFoundException("No records found");
            }
            var stocks = dbStock.Select(s => GetServiceStock(s)).ToList();

            return stocks;
        }

        public async Task<StockAdapter> GetStockById(StockAdapter stockAdapter)
        {
            var dbStock = await _dbContext.Stocks.Where(t => t.StockId == stockAdapter.StockId).FirstOrDefaultAsync();
            if (dbStock is null)
            {
                throw new ApplicationException($"No record found with the id ({stockAdapter.StockId})");
            }
            var stockList = GetServiceStock(dbStock);
            return stockList;
        }



    }
}