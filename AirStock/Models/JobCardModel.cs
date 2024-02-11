using System.Text;
using System.ComponentModel.DataAnnotations;
using AirStock.DAL;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace AirStock.Models
{
    public class JobCardModel
    {
        public int JobCardId { get; set; }
        public string BriefDescriptionOfWork { get; set; }
        public string SpareForService { get; set; }
        public string JobCardQuantity { get; set; }
        public int Cost { get; set; } // Corrected property name to start with an uppercase letter
        public string FittedBy { get; set; }
        public string Total { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string ChiefMechanicSignature { get; set; }

        public int VehicleId { get; set; }
        public VehicleModel Vehicle { get; set; }
        public List<VehicleModel> Vehicles { get; set; }

        public List<SparePart> SpareParts { get; set; }
    }
}
