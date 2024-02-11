using AirStock.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AirStock.Repositories;
using Microsoft.AspNetCore.Authorization;
using AirStock.DAL;
using AirStock.Services;
using AirStock.Common;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace AirStock.Controllers
{
    //[Authorize]
    //[ApiController]
    //[Route("api/[Controller]")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRegistrationService _registrationService;
        private readonly IUserRepositoryService _repoService;
        private readonly IRoleAssignmentService _roleAssignmentService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserRegistrationService registrationService, IUserRepositoryService userRepositoryService, IConfiguration configuration, IRoleAssignmentService roleAssignmentService )
        {
            _configuration = configuration;
            _registrationService = registrationService;
            _repoService = userRepositoryService;
            _roleAssignmentService = roleAssignmentService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


       

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}