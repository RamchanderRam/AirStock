using Microsoft.AspNetCore.Identity;
using AirStock.Models;

#nullable disable

namespace AirStock.Models
{
    public class ApplicationUser : IdentityUser
    {
        // You can add custom properties for your users here
        // For example, if you want to store additional user data:

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string PasswordHash { get; set; }


        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        //public DateTime LockOutEnd { get; set; }

        //public bool LockOutEnabled { get; set; }

        public bool AccessFailedCount { get; set; }
    }
}
