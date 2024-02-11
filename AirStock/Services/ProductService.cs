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

namespace AirStock.Services
{
    public class ProductService : IProductService
    {
        readonly StockContext _dbContext;
        private IServiceRoutine _sr;
        private IConfiguration _config;
        //private readonly IUserRepository _userRepository;

        public ProductService(StockContext dbContext, IConfiguration config, IServiceRoutine sr, IHttpContextAccessor contextAccessor)
        {
            _sr = sr;
            _config = config;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateProduct(ProductAdapter productAdapter)
        {
            var entityClient = GetEntityProduct(productAdapter);
            //entityClient.CreatedOn = DateRoutine.GetCurrentDate();
            _dbContext.Products.Add(entityClient);
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.InnerException?.ToString()); }

        }
        public async Task<bool> UpdateProduct(ProductAdapter productAdapter)
        {
            var dbExclusion = _dbContext.Products.Find(productAdapter.ProductId);

            if (dbExclusion is null)
            {
                throw new ApplicationException($"no record found with the id ({productAdapter.ProductId})");
            }
            var entityExclusion = GetEntityProduct(productAdapter);
            entityExclusion.ProductId = dbExclusion.ProductId;
            //entityExclusion.CreatedOn = dbExclusion.CreatedOn;
            //entityExclusion.CreatedBy = dbExclusion.CreatedBy;
            //entityExclusion.UpdatedBy = exclusionAdapter?.CurrentUserLoginId;
            try
            {
                _dbContext.Entry(dbExclusion).CurrentValues.SetValues(entityExclusion);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.InnerException?.ToString()); }
        }
        public async Task<bool> DeleteProduct(ProductAdapter productAdapter)
        {
            var dbExclusion = await _dbContext.Products.FindAsync(productAdapter.ProductId);

            if (dbExclusion is null)
            {
                throw new ApplicationException($"no record found with the id ({productAdapter.ProductId})");
            }
            _dbContext.Products.Remove(dbExclusion);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) { throw new Exception(ex.InnerException?.ToString()); }


            return true;
        }
        public async Task<ProductAdapter> GetProductById(ProductAdapter productAdapter)
        {
            var exclusionList = new ProductAdapter();

            var dbExclusion = await _dbContext.Products.Where(t => t.ProductId == productAdapter.ProductId).FirstOrDefaultAsync();
            if (dbExclusion is null)
            {
                throw new ApplicationException($"no record found with the id ({productAdapter.ProductId})");
            }
            exclusionList = GetServiceProduct(dbExclusion);

            return exclusionList;
        }

        public async Task<IPagedList<ProductAdapter>> GetProducts(ProductAdapter productAdapter, int pageNumber, int pageSize, string searchTerm )
        {
            //var ExclusionList = new IPagedList<ProductAdapter>();

            var dbProduct = await _dbContext.Products.ToListAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                dbProduct = dbProduct.Where(p => p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!dbProduct?.Any() ?? true)
            {
                throw new KeyNotFoundException("No records found");
            }
            var products = dbProduct.Select(s => GetServiceProduct(s)).ToPagedList(pageNumber, pageSize);
            return products;
        }




        public async Task<bool> DeleteProducts(ProductAdapter productAdapter)
        {
            var dbExclusions = _dbContext.Products.Where(s => productAdapter.ProductId == s.ProductId).FirstOrDefault();

            if (dbExclusions is null)
            {
                throw new ApplicationException($"No record found with the ids ");
            }
            _dbContext.Products.RemoveRange(dbExclusions);
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
        private NewProduct GetEntityProduct(ProductAdapter productAdapter)
        {
            var entity = new NewProduct()
            {
                ProductId = productAdapter.ProductId,
                ProductName = productAdapter.ProductName,
                ProductQuantity = productAdapter.ProductQuantity,
               
            };
           
            return entity;

        }
        private ProductAdapter GetServiceProduct(NewProduct productAdapter)
        {
            var model = new ProductAdapter()

            {
                ProductId = productAdapter.ProductId,
                ProductName = productAdapter.ProductName,
                ProductQuantity = productAdapter.ProductQuantity
           
              
            };

            return model;

        }

    }
}
