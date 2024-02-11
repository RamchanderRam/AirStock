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
using Microsoft.AspNetCore.Authorization;

namespace AirStock.Controllers
{
    //[Route("api/[controller]/[action]")]
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize(Policy = "UserPolicy")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private IServiceRoutine _sr;

        public ProductController(IProductService productService, ILogger<ProductController> logger, IServiceRoutine serviceRoutine)
        {
            _sr = serviceRoutine;
            _productService = productService;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        /// <summary>

        [HttpGet("product")]
        public async Task<IActionResult> GetProducts(int? page, string? searchTerm = null /*, int pageSize=5*/ )
        {
            ProductAdapter productAdapter = new ProductAdapter();

            int pageNumber = page ?? 1;
            int pageSize = 10; // Number of items per page
            IPagedList<ProductAdapter> products = await _productService.GetProducts(productAdapter, pageNumber, pageSize, searchTerm);

            return View(products);
        }


        /// <summary>

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id == 0)
            {
                throw new ApplicationException("invalid input for id");
            }
            ProductAdapter exclusionAdapter = new ProductAdapter { ProductId = id };
            var result = await _productService.GetProductById(exclusionAdapter);
            return View(result);
        }

        [HttpGet] // Use GET for rendering the view
        public async Task<IActionResult> Create()
        {

            return View("CreateProduct");
        }


        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([FromForm] ProductAdapter exclusionAdapter)
        {
            //if (!ModelState.IsValid)
            //{

            //exclusionAdapter.CurrentUserLoginId = _currentUser.LoginId;
            var result = await _productService.CreateProduct(exclusionAdapter);
            //return Ok(new { message = ResponseReturnCode.SUCCESS });

            //}
            return RedirectToAction("GetProducts");

        }

        [HttpGet("UpdateProduct/{id}")] // Use GET for rendering the view
        public IActionResult Update(int id)
        {
            ProductAdapter productAdapter = new ProductAdapter();
                    productAdapter.ProductId = id;
            return View("UpdateProduct", productAdapter);
        }

        [HttpPost("UpdateProduct/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductAdapter productAdapter)
        {
            if (id == 0)
            {
                throw new ApplicationException("invalid input for id");
            }
            if ( ModelState.IsValid)
            {
                productAdapter.ProductId = id;
                productAdapter.ProductName = productAdapter.ProductName;
                productAdapter.ProductQuantity = productAdapter.ProductQuantity;
                //exclusionAdapter.CurrentUserLoginId = _currentUser.LoginId;
                var result = await _productService.UpdateProduct(productAdapter);
                //return View("UpdateProduct", productAdapter);
            }
            return RedirectToAction("GetProducts");

        }
        /// <summary>
        /// delete Exclusion

        [HttpGet("Delete/{id}")] // Use GET for rendering the view
        public async Task<IActionResult> Delete(int id)
        {
            ProductAdapter productAdapter = new ProductAdapter();
            productAdapter.ProductId = id;
            var result = await _productService.GetProductById(productAdapter);
            return View("Delete", result);
        }


        //[HttpDelete("{id}")]
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
            {
                throw new ApplicationException("invalid input in id");
            }
            ProductAdapter productAdapter = new ProductAdapter { ProductId = id    };
            var result = await _productService.DeleteProduct(productAdapter);
            return RedirectToAction("GetProducts"); // Redirect to product list after deletion
        }
    }
}
