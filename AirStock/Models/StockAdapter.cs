using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel.DataAnnotations;
using AirStock.DAL;
#nullable disable


namespace AirStock.Models
{
    public class StockAdapter
    {
        public int StockId { get; set; }    

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int ProductQuantity { get; set;}

        public DateTime? ProductDate { get; set; }

        public bool IsActive { get; set; }

        public List<StockAdapter> stockAdapters { get; set; }/* = new List<StockAdapter>();*/
    }
}
