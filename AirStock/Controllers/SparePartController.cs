using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Add this line
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
using Microsoft.AspNetCore.Authorization;


namespace AirStock.Controllers
{
    //[Route("api/[controller]/[action]")]
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize(Policy = "UserPolicy")]
    public class SparePartController : Controller
    {
        private readonly ISparePartService _sparePartService;
        private readonly IJobCardService  _jobCardService;

        private IServiceRoutine _sr;

        public SparePartController(ISparePartService sparePartService, IJobCardService jobCardService, ILogger<SparePartController> logger, IServiceRoutine serviceRoutine)
        {
            _sr = serviceRoutine;
            _sparePartService = sparePartService;
            _jobCardService = jobCardService;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        /// <summary>

        [HttpGet("sparePart")]
        public async Task<IActionResult> GetSpareParts(int? page, string? searchTerm = null /*, int pageSize=5*/ )
        {
            var jobCards = await _jobCardService.GetJobCards();
            var sparePartViewModel = new SparePartViewModel
            {
                JobCardOptions = new SelectList(jobCards, nameof(JobCardModel.JobCardId), nameof(JobCardModel.BriefDescriptionOfWork)),
            };
            SparePartModel sparePart = new SparePartModel();

            int pageNumber = page ?? 1;
            int pageSize = 10; // Number of items per page
            IPagedList<SparePartModel> products = await _sparePartService.GetSpareParts(sparePart, pageNumber, pageSize, searchTerm);

            return View(products);
        }


        /// <summary>

        [HttpGet("sparePart/{id}")]
        public async Task<IActionResult> GetSparePartById(SparePartModel sparePartModel)
        {
            if (sparePartModel.SparePartId==0)
            {
                throw new ApplicationException("invalid input for id");
            }
            SparePartModel exclusionAdapter = new SparePartModel { SparePartId = sparePartModel.SparePartId };
            var result = await _sparePartService.GetByIdAsync(exclusionAdapter.SparePartId);
            return View(result);
        }

        [HttpGet("")] // Use GET for rendering the view
        public async Task<IActionResult> CreateSparePart()
        {
            // Your logic to fetch existing vehicles, if needed
            var jobCards = await _jobCardService.GetJobCards(); // Await the asynchronous method
            ViewBag.JobCards = new SelectList(jobCards, "JobCardId", "BriefDescriptionOfWork");
            var viewModel = new SparePartViewModel
            {
                JobCards = new SelectList(jobCards, "JobCardId", "BriefDescriptionOfWork"),
                SpareParts = new List<SparePartModel>(), // Initialize SpareParts here
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSparePart([FromForm] SparePartViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sparePartModel = new SparePartModel
                    {
                        Name = viewModel.SparePart.Name,
                        Quantity = viewModel.SparePart.Quantity,
                        SparePartCost = viewModel.SparePart.SparePartCost,
                        SparePartAmount = viewModel.SparePart.SparePartAmount,
                        JobCardId = viewModel.SparePart.JobCardId
                        // Add other properties as needed
                    };

                    await _sparePartService.CreateSparePart(sparePartModel);
                    return RedirectToAction("GetSpareParts"); // Redirect to a suitable action
                }
                catch (InvalidOperationException ex)
                {
                    // Handle the case where the associated JobCard doesn't exist
                    ModelState.AddModelError("SparePart.JobCardId", ex.Message);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    ModelState.AddModelError(string.Empty, $"Error creating SparePart: {ex.Message}");
                }
            }

            // Repopulate vehicles dropdown on validation failure
            var jobCards = await _jobCardService.GetJobCards();
            viewModel.JobCards = new SelectList(jobCards, "JobCardId", "BriefDescriptionOfWork", viewModel.SparePart.JobCardId);

            return View(viewModel);
        }




        [HttpGet("UpdateSparePart/{id}")] // Use GET for rendering the view
        public IActionResult Update(int id)
        {
            SparePartModel productAdapter = new SparePartModel();
            productAdapter.SparePartId = id;
            return View("UpdateSparePart", productAdapter);
        }

        [HttpPost("UpdateSparePart/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSparePart(int id, [FromForm] SparePartModel productAdapter)
        {
            if (id == 0)
            {
                throw new ApplicationException("invalid input for id");
            }
            if (ModelState.IsValid)
            {
                productAdapter.SparePartId = id;
                productAdapter.Name = productAdapter.Name;
                productAdapter.Quantity = productAdapter.Quantity;
                productAdapter.SparePartCost = productAdapter.SparePartCost;
                productAdapter.SparePartAmount = productAdapter.SparePartAmount;

                //exclusionAdapter.CurrentUserLoginId = _currentUser.LoginId;
                var result = await _sparePartService.UpdateSparePart(productAdapter);
                //return View("UpdateProduct", productAdapter);
            }
            return RedirectToAction("GetSpareParts");

        }
        /// <summary>
        /// delete Exclusion

        [HttpGet("Delete/{id}")] // Use GET for rendering the view
        public async Task<IActionResult> Delete(int id)
        {
            SparePartModel productAdapter = new SparePartModel();
            productAdapter.SparePartId = id;
            var result = await _sparePartService.RemoveSparePart(productAdapter);
            return View("Delete", result);
        }


    }
}

