using System.Text;
using System.ComponentModel.DataAnnotations;
using AirStock.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;

#nullable disable

namespace AirStock.Models
{
    public class RoleAssignmentViewModel
    {
        [Required(ErrorMessage = "Please select a role.")]
        public string SelectedRole { get; set; }

        [Required(ErrorMessage = "Please select a user.")]
        public string SelectedUser { get; set; }


        public string UserName { get; set; }
        public string RoleName { get; set; }


        // Add a property for Roles
        public List<SelectListItem> Roles { get; set; }

        // Add a property for Users
        public List<SelectListItem> Users { get; set; }

        // Add the AvailableRoles property
        public IEnumerable<SelectListItem> AvailableRoles { get; set; }

        // Add the AvailableUsers property
        public IEnumerable<SelectListItem> AvailableUsers { get; set; }

    }

}
