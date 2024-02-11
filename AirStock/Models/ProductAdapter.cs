using System.Text;
using System.ComponentModel.DataAnnotations;
using AirStock.DAL;
#nullable disable

namespace AirStock.Models
{
    public class ProductAdapter
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }

        public string SearchTerm { get; set; }

        //public ProductAdapter productAdapters { get; set; } /*= new List<ProductAdapter>();*/

    }
}
