using System.ComponentModel.DataAnnotations;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AirStock.DAL;

#nullable disable
namespace AirStock.Models
{
    public class UserModelAdapter
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Username field is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password field is required.")]
        public string PasswordHash { get; set; }
        public string Email { get; set; }


        //public int UserRoleId { get; set; }

        public string RoleName { get; set; }

        public List<UserModelAdapter> Users { get; set; }


        public bool EmailConfirmed { get; set; }

        public string  PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime LockOutEnd { get; set; }

        public bool LockOutEnabled { get; set; }

        public bool AccessFailedCount { get; set; }

        public string SelectedRole { get; set; }

    }

    // AdminDashboardModel.cs
    public class AdminDashboardModel
    {
        public List<UserModel> Users { get; set; }
        // Add other properties as needed
    }

}
