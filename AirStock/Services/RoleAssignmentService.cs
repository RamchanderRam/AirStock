using AirStock.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AirStock.Models;
using AirStock.Common.Models;
using AirStock.DAL;
using Microsoft.AspNetCore.Identity;


namespace AirStock.Services
{
    public class RoleAssignmentService : IRoleAssignmentService
    {

        readonly StockContext _dbContext;
        private IServiceRoutine _sr;
        private IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager; // Replace YourUserType with your user type
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleAssignmentService(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager, StockContext dbContext, IConfiguration config, IServiceRoutine sr, IHttpContextAccessor contextAccessor)
        {
            _sr = sr;
            _config = config;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public bool ShouldAssignUserRole(string username)
        {
            // Example logic: Check if the username contains "user" or "customer."
            // If it does, assign the "User" role; otherwise, do not assign it.
            return username.Contains("user") || username.Contains("customer");
        }

        public bool ShouldAssignAdminRole(string username)
        {
            // Example logic: Check if the username is "admin."
            // If it is, assign the "Admin" role; otherwise, do not assign it.
            return username.Equals("admin", StringComparison.OrdinalIgnoreCase);
        }

        // Implement the method from the interface
        public async Task<bool> AssignRoleToUserAsync(string username, string role)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("Username and roleName are required.");
            }

            //var user = await _userManager.FindByNameAsync(username);
            var normalizedUsername = username.ToLower();
            //var normalizedUsername = _userManager.NormalizeName(username);
            //var user = await _userManager.FindByNameAsync(normalizedUsername);
            var user = await _userManager.FindByNameAsync(normalizedUsername);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            
            if (result == null)
            {
                // Handle the situation where result is null
                throw new InvalidOperationException("Role assignment operation returned null result.");
            }

            if (!result.Succeeded)
            {
                // Handle role assignment failure, e.g., by logging and returning an error.
                throw new InvalidOperationException("Role assignment failed.");
            }
            return true;

        }

        // Implement the GetRoles method
        public IEnumerable<string> GetRoles()
        {
            // Retrieve the list of roles from your data source
            var roles = _roleManager.Roles.Select(role => role.Name).ToList();
            return roles;
        }

        public List<string> Roles
        {
            get
            {
                // You need to provide the logic to fetch roles from your data source
                // For example, if you're using ASP.NET Identity, you can fetch roles like this:
                return _roleManager.Roles.Select(r => r.Name).ToList();
            }
        }

        public async Task<bool> RoleExistsAsync(string roleName, string userName)
        {
            // Use Entity Framework to check if the role exists
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            
            // If a role with the given name is found, it exists; otherwise, it does not exist
            return role != null && user !=null;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            var role = new IdentityRole { Name = roleName };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }
    }
}
