using AirStock.Models;
using AirStock.Repositories;
using AirStock.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AirStock.DAL;
using AirStock.Controllers;
using AirStock.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using AirStock.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//builder.Services.AddMiddleware();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<StockContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("StockConnection")));
options.UseSqlServer(builder.Configuration.GetConnectionString("StockConnection")));



//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<StockContext>()
//    .AddDefaultTokenProviders();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Configure identity options here
//    options.Password.RequiredLength = 8;
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequireNonAlphanumeric = true;

//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    options.User.RequireUniqueEmail = true;
//    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

//    options.SignIn.RequireConfirmedAccount = true;
//    options.SignIn.RequireConfirmedEmail = true;
//    options.SignIn.RequireConfirmedPhoneNumber = false;
//});



builder.Services.AddHttpContextAccessor();
builder.Services.AddMvcCore();
builder.Services.AddCors();
builder.Services.AddSingleton<IGetAccessToken, GetAccessToken>();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IServiceRoutine, ServiceRoutine>();
builder.Services.AddScoped<IGetAccessToken, GetAccessToken>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<IUserRepositoryService, UserRepositoryService>();
builder.Services.AddScoped<IRoleAssignmentService, RoleAssignmentService>();
//builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();
builder.Services.AddScoped<IRoleAssignmentService, RoleAssignmentService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IJobCardService, JobCardService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ISparePartService, SparePartService>();
builder.Services.AddScoped<ILogger, Logger<AccountController>>();

//Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        //options.DefaultScheme = IdentityConstants.ApplicationScheme;
        //options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

        };
    });

//var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
//var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//var claims = new List<Claim>();

//var token = new JwtSecurityToken(
//    issuer: builder.Configuration["Jwt:Issuer"],
//    audience: builder.Configuration["Jwt:Audience"],
//    claims: claims,
//    expires: DateTime.Now.AddMinutes(30), // Set token expiration as per your requirements
//    signingCredentials: creds
//);

//var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//            .AddCookie(options =>
//            {
//                options.Cookie.Name = "YourAuthCookieName"; // Change to your desired cookie name
//                options.Cookie.HttpOnly = true;
//                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Adjust the expiration time as needed
//                options.LoginPath = "/Account/Login"; // The path where login should redirect if authentication fails
//                options.AccessDeniedPath = "/Account/AccessDenied"; // The path for access denied responses
//                options.SlidingExpiration = true; // Sliding expiration to extend the cookie lifetime
//            });

//        // ... other service configurations ...


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<StockContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware();
app.MapControllers();

//app.UseEndpoints(routes =>
//{
//    routes.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//});

// Route for registration should be defined before routes with [Authorize]
app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}",
        defaults: new { controller = "Home", action = "Index" });

    // Add more routes as needed...
});

app.Run();


