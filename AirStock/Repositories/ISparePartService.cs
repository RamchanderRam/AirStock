using AirStock.DAL;
using AirStock.Models;
using PagedList;

namespace AirStock.Repositories
{


    public interface ISparePartService
    {

        /// <summary>
        /// Get Job Cards based on the provided JobCardModel
        /// </summary>
        /// <param name="jobCardModel"></param>
        /// <returns></returns>
        Task<IPagedList<SparePartModel>> GetSpareParts(SparePartModel sparePart, int pageNumber, int pageSize, string searchTerm);

        /// <summary>
        /// Get JobCard by Id asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SparePartModel> GetByIdAsync(int id);

        /// <summary>
        /// Add JobCard asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> CreateSparePart(SparePartModel entity);

        /// <summary>
        /// Update JobCard asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateSparePart(SparePartModel entity);

        /// <summary>
        /// Remove JobCard asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> RemoveSparePart(SparePartModel entity);

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

    }
}
