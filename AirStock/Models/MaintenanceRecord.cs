using System.Text;
using System.ComponentModel.DataAnnotations;
using AirStock.DAL;
#nullable disable

namespace AirStock.Models
{
    public class MaintenanceRecord
    {
        public DateTime Date { get; set; }
        public string Type { get; set; } // In-house or External
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public string MakeModel { get; set; }
        public int Year { get; set; }
        public string PlateNo { get; set; }
        public string FleetNo { get; set; }
        // Other properties related to the form fields

        public string Complaint { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal Discount { get; set; }
    }
}
