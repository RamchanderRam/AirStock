using AirStock.DAL;
using AirStock.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirStock.Repositories
{

    public interface IJobCardService
    {
        /// <summary>
        /// Get Job Cards based on the provided JobCardModel
        /// </summary>
        /// <param name="jobCardModel"></param>
        /// <returns></returns>
        Task<List<JobCardModel>> GetJobCards(JobCardModel jobCardModel = null);

        /// <summary>
        /// Get JobCard by Id asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JobCardModel> GetByIdAsync(int id);

        /// <summary>
        /// Add JobCard asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> CreateJobCard(JobCardModel entity);

        /// <summary>
        /// Update JobCard asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateJobCard(JobCardModel entity);

        /// <summary>
        /// Remove JobCard asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> RemoveJobCard(JobCardModel entity);

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
