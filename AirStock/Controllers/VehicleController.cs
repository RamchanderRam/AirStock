using Microsoft.AspNetCore.Mvc;
using AirStock.Services; // Replace YourNamespace with the appropriate namespace for VehicleService
using AirStock.Models;
using AirStock.Common.Models;
using AirStock.Repositories;
using AirStock.DAL;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;
using PagedList;
using PagedList.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

#nullable disable

namespace AirStock.Controllers
{
    
        [ApiController]
        [Route("api/[Controller]")]
        //[Authorize(Policy = "UserPolicy")]
        public class VehicleController : Controller
        {
            private readonly IVehicleService _vehicleService;
            private IServiceRoutine _sr;

        public VehicleController(IVehicleService vehicleService, ILogger<VehicleController> logger, IServiceRoutine serviceRoutine)
        {
            _sr = serviceRoutine;
            _vehicleService = vehicleService;
        }

        //    public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet] // Use GET for rendering the view
        public async Task<IActionResult> Create()
        {
            VehicleModel vehicleModel = new VehicleModel();
            var result =  _vehicleService.GetVehicleData();
            //return View(result);
            return View("CreateVehicle");
        }


        [HttpPost("CreateVehicle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVehicle([FromForm] VehicleModel vehicleModel)
        {
            //if (!ModelState.IsValid)
            //{

            //exclusionAdapter.CurrentUserLoginId = _currentUser.LoginId;
            var result = await _vehicleService.CreateVehicle(vehicleModel);
            //return Ok(new { message = ResponseReturnCode.SUCCESS });

            //}
            return RedirectToAction("GetVehicles");

        }

        [HttpGet("vehicles")]
        public async Task<IActionResult> GetVehicles()
        {
            List<VehicleModel> result = new List<VehicleModel>  ();
            try
            {
                result = _vehicleService.GetVehicleData();
            }
            catch (Exception ex) { }
            return View(result);
        }
    }
}
