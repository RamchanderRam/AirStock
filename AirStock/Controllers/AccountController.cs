using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AirStock.DAL;
using AirStock.Common.Models;
using AirStock.Repositories;
using AirStock.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace AirStock.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize(Policy = "UserPolicy")]

    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private IServiceRoutine _sr;
        private readonly IUserRegistrationService _registrationService;

        public AccountController(IUserRegistrationService registrationService, IConfiguration configuration, ILogger<AccountController> logger, IServiceRoutine serviceRoutine)
        {
            _sr = serviceRoutine;
            _configuration = configuration;
            _registrationService = registrationService;
        }

        [Authorize(Policy = "UserPolicy")]
        public IActionResult UserDashboard()
        {
            // Get the currently authenticated user's identity
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            // Retrieve user-specific data from your database or service
            var userProfile = _registrationService.GetUserProfile(identity.Name);

            // Pass the user profile data to the view
            return View(userProfile);
        }


        [HttpGet("login")]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();
        }

        //[HttpPost("login")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        //{
        //    //var accountModel = new UserModelAdapter();
        //    //ViewData["Title"] = "Login";            // Validate user credentials (replace with your authentication logic)
        //    if (IsValidUser(username, password))
        //    {
        //        // Create claims
        //        var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.Name, username),
        //    // Add roles or other claims as needed
        //};

        //        // Check if the user should have the "User" role and add it to claims
        //        if (ShouldAssignUserRole(username))
        //        {
        //            claims.Add(new Claim(ClaimTypes.Role, "User"));
        //        }

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var token = new JwtSecurityToken(
        //            issuer: _configuration["Jwt:Issuer"],
        //            audience: _configuration["Jwt:Audience"],
        //            claims: claims,
        //            expires: DateTime.Now.AddMinutes(30), // Token expiration time
        //            signingCredentials: creds
        //        );

        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var tokenString = tokenHandler.WriteToken(token);

        //        // Create an instance of UserModelAdapter and populate it with user data
        //        var accountModel = new UserModelAdapter();


        //        // Set the JWT token as a cookie or return it to the client as needed
        //        Response.Cookies.Append("access_token", tokenString, new CookieOptions
        //        {
        //            HttpOnly = true,
        //            Secure = true, // Make sure to use HTTPS in production
        //            SameSite = SameSiteMode.Strict
        //        });

        //        //return RedirectToAction("Index", "Home"); // Redirect to the desired page
        //        return RedirectToAction("Login"); // Redirect to the "Login" page

        //    }
        //    // Handle invalid login here
        //    ModelState.AddModelError(string.Empty, "Invalid login attempt");
        //    var emptyAccountModel = new UserModelAdapter()
        //    {
        //        Username = "", // Set default or empty values for properties
        //        Password = ""

        //    };

        //    // Create an empty UserModelAdapter
        //    //return View(emptyAccountModel); // Return the empty UserModelAdapter
        //    return RedirectToAction("Controller", "Action"); // Redirect to the "Login" page

        //}

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            if (IsValidUser(username, password))
            {
                // Create claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            // Add roles or other claims as needed
        };

                // Check if the user should have the "User" role and add it to claims
                if (ShouldAssignUserRole(username))
                {
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                }
                else
                {
                    // Optionally, assign other roles if needed
                    // claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }

                // Create a new ClaimsIdentity
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign in the user
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirect based on user's role
                if (User.IsInRole("User"))
                {
                    return RedirectToAction("UserDashboard", "Account"); // Redirect to the user dashboard
                }
                else if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("AdminDashboard", "Admin"); // Redirect to the admin dashboard
                }
                else
                {
                    // Handle other roles or scenarios
                    return RedirectToAction("DefaultAction", "DefaultController");
                }
            }
            // Handle invalid login here
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            //return RedirectToAction("Login"); // Redirect to the "Login" page
            return View("Login");
        }


        // You can implement the logic to determine if the user should have the "User" role
        private bool ShouldAssignUserRole(string username)
        {
            // Implement your logic to determine if the user should have the "User" role here.
            // For example, you can check a database or other criteria.
            return true; // Assign the "User" role in this example
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Remove the JWT token cookie  
            Response.Cookies.Delete("access_token");

            return RedirectToAction("Index", "Home"); // Redirect to the desired page
        }

        private bool IsValidUser(string username, string password)
        {
            // Implement your user validation logic here (e.g., check against a database)
            // Return true if valid, otherwise false
            return (username == "demo" && password == "demo");
        }




        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUserAsync([FromForm] UserModelAdapter userModel)
        {
            if (ModelState.IsValid)
            {
                var registrationResult = await _registrationService.RegisterUserAsync(userModel);

                if (registrationResult)
                {
                    // Registration successful, redirect to login page or another appropriate page.
                    return RedirectToAction("Login");
                }
                else
                {
                    // Registration failed, add an error message to the model state.
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
                }
            }

            // If the model state is not valid or registration fails, return to the registration page.
            return View(userModel);
        }
    }
}
