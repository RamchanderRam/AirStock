using System.Text;
using System.ComponentModel.DataAnnotations;
using AirStock.DAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
#nullable disable

namespace AirStock.Models
{
    public class SparePartViewModel
    {
        public SparePartModel SparePart { get; set; }
        //public List<SelectListItem> JobCards { get; set; }

        //public List<SparePartModel> SpareParts { get; set; } = new List<SparePartModel>();

        public SelectList JobCardOptions { get; set; }

        public IEnumerable<SelectListItem> Vehicles { get; set; }
        public List<JobCardModel> jobCards { get; set; }

        public List<VehicleModel> VehicleModels { get; set; } // Ensure this property is defined
        public List<SparePartModel> SpareParts { get; set; } = new List<SparePartModel>();
        //public IEnumerable<SelectListItem> JobSpareParts { get; set; }
        public List<SelectListItem> JobSpareParts { get; set; }
        public IEnumerable<SelectListItem> JobCards { get; set; }

    }
}
