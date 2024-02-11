using AirStock.Models;
using Microsoft.AspNetCore.Mvc;
using AirStock.Common.Models;
using AirStock.Repositories;
using AirStock.Services;
using AirStock.DAL;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;
using PagedList;
using PagedList.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace AirStock.Controllers
{
    public class MaintenanceController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}


        [HttpGet]
        public IActionResult VehicleMaintenance()
        {
            // Logic to display vehicle maintenance form
            return View();
        }

        [HttpPost]
        public IActionResult VehicleMaintenance(MaintenanceRecord maintenanceRecord)
        {
            // Logic to handle submitted vehicle maintenance form
            // Save maintenance record to the database
            return RedirectToAction("Index", "Home"); // Redirect to home or relevant page
        }

        [HttpGet]
        public IActionResult MachineryMaintenance()
        {
            // Logic to display machinery maintenance form
            return View();
        }

        [HttpPost]
        public IActionResult MachineryMaintenance(MaintenanceRecord maintenanceRecord)
        {
            // Logic to handle submitted machinery maintenance form
            // Save maintenance record to the database
            return RedirectToAction("Index", "Home"); // Redirect to home or relevant page
        }
    }
}
