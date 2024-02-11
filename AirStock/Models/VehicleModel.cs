using System.Text;
using System.ComponentModel.DataAnnotations;
using AirStock.DAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable


namespace AirStock.Models
{
    public class VehicleModel
    {
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public string FleetNumber { get; set; }
        public string Division { get; set; }
        public int DriverKm { get; set; }
        public DateTime? DateOfLastService { get; set; }
        public List<JobCardModel> JobCards { get; set; }

    }
}
