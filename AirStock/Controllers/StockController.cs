using AirStock.Models;
using AirStock.Repositories;
using AirStock.Services;
using AirStock.Controllers;
using AirStock.DAL;
using Microsoft.AspNetCore.Mvc;
using AirStock.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AirStock.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IServiceRoutine _sr;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StockController(IStockService stockService, ILogger<StockController> logger, IConfiguration config, IServiceRoutine sr, IHttpContextAccessor httpContextAccessor)
        {
            _stockService = stockService;
            _config = config;
            _sr = sr;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>

        [HttpGet("stock")]
        public async Task<IActionResult> GetStock()
        {
            StockAdapter stockAdapter = new StockAdapter();
            var result = await _stockService.GetStock(stockAdapter);
            return View(result); // Pass the result directly to the view
        }


        /// <summary>

        [HttpGet("stock/{id}")]
        public async Task<IActionResult> GetStockById(int id)
        {
            if (id == 0)
            {
                throw new ApplicationException("invalid input for id");
            }
            //StockAdapter stockAdapter = new StockAdapter
            StockAdapter stockAdapter = await _stockService.GetStockById(new StockAdapter { StockId = id });

            //StockAdapter stockAdapter = new StockAdapter

            //{
            //    StockId = id,
            //    //CurrentUserLoginId = _currentUser.LoginId
            //};
            //var result = await _stockService.GetStockById(stockAdapter);
            return View(stockAdapter);
        }

        [HttpGet] // Use GET for rendering the view
        public async Task<IActionResult> Create()
        {

            return View("CreateStock");
        }

        /// <summary>
        /// add Appointment
        /// </summary>

        [HttpPost("CreateStock")]
        public async Task<IActionResult> CreateStock(StockAdapter stockAdapter)
        {
            if (string.IsNullOrEmpty(stockAdapter.ProductName))
            {
                throw new ApplicationException("Invalid input for Product Name");
            }

            //if ((appointmentAdapter.AppointmentstartTime != null && appointmentAdapter.AppointmentEndTime != null) && (appointmentAdapter.AppointmentstartTime.Value > appointmentAdapter.AppointmentEndTime.Value))
            //{
            //    throw new ApplicationException("invalid start/end time.");
            //}
            //appointmentAdapter.CurrentUserLoginId = _currentUser.LoginId;
            await _stockService.AddStock(stockAdapter);
            //return Ok(new { message = ResponseReturnCode.SUCCESS });
            return View(stockAdapter);
        }
        /// <summary>


        /// <summary>
        /// update Stock
        /// </summary>
      
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, StockAdapter stockAdapter)
        {
            if (string.IsNullOrEmpty(stockAdapter.ProductName) || id == 0 )
            {
                throw new ApplicationException("invalid input in ModificationDetails/type & id");
            }
            stockAdapter.StockId = id;
            //stockAdapter.CurrentUserLoginId = _currentUser.LoginId;
            await _stockService.UpdateStock(stockAdapter);
            //return Ok(new { message = ResponseReturnCode.SUCCESS });
            return View();
        }
        /// <summary>
        /// delete Stock
        /// </summary>
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                throw new ApplicationException("invalid input in id");
            }
            StockAdapter stockAdapter = new StockAdapter { StockId = id };
            await _stockService.DeleteStock(stockAdapter);
            //return Ok(new { message = ResponseReturnCode.SUCCESS });

            return View();
        }
        
    }
}
