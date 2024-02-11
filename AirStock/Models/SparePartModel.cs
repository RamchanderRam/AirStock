using System.Text;
using System.ComponentModel.DataAnnotations;
using AirStock.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

#nullable disable

namespace AirStock.Models
{
    public class SparePartModel
    {
        [Key]
        public int SparePartId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string SparePartCost { get; set; }
        public string SparePartAmount { get; set; }
        public int JobCardId { get; set; }
        public JobCardModel JobCard { get; set; }
        public List<SelectListItem> JobSpareParts { get; set; }
        public int SelectedJobCardId { get; set; }
        //public List<SelectListItem> JobCards { get; set; }
        public List<JobCardModel> JobCards { get; set; }


    }
}
