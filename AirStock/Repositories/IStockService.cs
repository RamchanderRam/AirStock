using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AirStock.Models;
using AirStock.DAL;
using AirStock.Models;

namespace AirStock.Repositories
{

    public interface IStockService
    {
        /// <summary>
        /// Add StockService
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddStock(StockAdapter stock);
        /// <summary>
        /// update StockService based on the id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateStock(StockAdapter stock);
        /// <summary>
        /// delete Stock Service based on the id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> DeleteStock(StockAdapter stock);
        /// <summary>
        /// get Stock by id
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<StockAdapter> GetStockById(StockAdapter stock);
        /// <summary>
        /// get Stock list 
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns

        /// <summary>
        /// get Stock list
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns>
        Task<List<StockAdapter>> GetStock(StockAdapter stockAdapter);

    }
}
