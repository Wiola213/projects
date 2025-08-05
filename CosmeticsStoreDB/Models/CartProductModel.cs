using System.ComponentModel.DataAnnotations;

namespace CosmeticsStoreDB.Models
{
    public class CartProductModel
    {
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
