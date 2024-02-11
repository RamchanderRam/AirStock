using AirStock.DAL;
using AirStock.Models;
using PagedList;

namespace AirStock.Repositories
{


    public interface IProductService
    {
        
        Task<bool> CreateProduct(ProductAdapter product);
       
        Task<bool> UpdateProduct(ProductAdapter product);
       
        Task<bool> DeleteProduct(ProductAdapter product);
       
        Task<ProductAdapter> GetProductById(ProductAdapter product);


        Task<IPagedList<ProductAdapter>> GetProducts(ProductAdapter product, int pageNumber, int pageSize, string searchTerm);

    }
}
