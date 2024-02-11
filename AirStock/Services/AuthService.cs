using System.Threading.Tasks;
using AirStock.Common.Models;
using AirStock.Models;
using AirStock.DAL;
using AirStock.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AirStock.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepositoryService _userRepositoryService;
        readonly StockContext _dbContext;
        private IServiceRoutine _sr;
        private IConfiguration _config;
        public AuthService(IUserRepositoryService userRepositoryService, StockContext dbContext, IConfiguration config, IServiceRoutine sr, IHttpContextAccessor contextAccessor)
        {
            _sr = sr;
            _config = config;
            _dbContext = dbContext;
            _userRepositoryService = userRepositoryService;
        }


        public async Task<string> AuthenticateAsync(UserModelAdapter userModel)
        {
            // Validate user credentials and generate JWT token
            var user = await _userRepositoryService.GetUserAsync(userModel.Username, userModel.PasswordHash);

            if (user != null && user.Username == userModel.PasswordHash && user.PasswordHash == userModel.PasswordHash)
            {
                // Retrieve user roles (you can implement this in UserRepositoryService)
                var roles = await _userRepositoryService.GetUserRolesAsync(user);

                // Create claims
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

                // Add roles to claims if available
                if (roles != null && roles.Any())
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30), // Token expiration time
                    signingCredentials: creds
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenString = tokenHandler.WriteToken(token); // Generate JWT token

                return tokenString; // Return the generated token
            }

            return null; // Authentication failed
        }


    }
}
