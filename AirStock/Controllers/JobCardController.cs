using Microsoft.AspNetCore.Mvc;
using AirStock.Common.Models;
using AirStock.Models;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace AirStock.Controllers
{
    // JobCardController.cs
    [ApiController]
    [Route("api/[Controller]")]
    public class JobCardController : Controller
    {
        private readonly IJobCardService _jobCardService;
        private readonly IVehicleService _vehicleService;
        private IServiceRoutine _sr;

        public JobCardController(IJobCardService jobCardService, IVehicleService vehicleService, ILogger<ProductController> logger, IServiceRoutine serviceRoutine)
        {
            _sr = serviceRoutine;
            _jobCardService = jobCardService;
            _vehicleService = vehicleService;
        }

        [HttpGet("JobCard")]
        public async Task<IActionResult> GetJobCards([FromQuery] JobCardModel jobCardModel)
        {
            var jobCards = await _jobCardService.GetJobCards(jobCardModel);
            var vehicles = _vehicleService.GetVehicleData();

            var viewModel = new JobCardViewModel
            {
                JobCards = jobCards.AsEnumerable().ToList(),
                Vehicles = vehicles.Select(v => new SelectListItem { Value = v.VehicleId.ToString(), Text = v.VehicleName }).ToList()
            };

            return View(viewModel);
        }



        [HttpGet]
        public IActionResult CreateJobCard()
        {
            // Your logic to fetch existing vehicles, if needed
            var vehicles = _vehicleService.GetVehicleData();
            ViewBag.Vehicles = new SelectList(vehicles, "VehicleId", "VehicleName");
            var viewModel = new JobCardViewModel
            {
                Vehicles = new SelectList(vehicles, "VehicleId", "VehicleName"),
                SpareParts = new List<SparePartModel>(), // Initialize SpareParts here

            };

            return View(viewModel); // This should be of type JobCardViewModel, not JobCardModel
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateJobCard([FromForm] JobCardModel jobCard)
        {
            if (ModelState.IsValid)
            {
                _jobCardService.CreateJobCard(jobCard);
                return RedirectToAction("GetJobCards"); // Redirect to a suitable action
            }

            // Repopulate vehicles dropdown on validation failure
            var vehicles = _vehicleService.GetVehicleData();
            var viewModel = new JobCardViewModel
            {
                JobCard = jobCard,
                Vehicles = new SelectList(vehicles, "VehicleId", "VehicleName", jobCard.VehicleId)
            };

            return View(viewModel);
        }



        //[HttpGet]
        //public IActionResult CreateJobCard()

        //{
        //    // Your logic to fetch existing vehicles, if needed
        //    var vehicles = _vehicleService.GetVehicleData();

        //    ViewBag.Vehicles = new SelectList(vehicles, "VehicleId", "VehicleName");

        //    return View();
        //}

        //[HttpPost()]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateJobCard([FromForm] JobCardModel jobCard)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _jobCardService.CreateJobCard(jobCard);
        //        return RedirectToAction("GetJobCards"); // Redirect to a suitable action
        //    }

        //    // Repopulate vehicles dropdown on validation failure
        //    var vehicles = _vehicleService.GetVehicleData();
        //    ViewBag.Vehicles = new SelectList(vehicles, "Id", "VehicleName", jobCard.VehicleId);

        //    return View(jobCard);
        //}


    }
}