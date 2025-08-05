using System.ComponentModel.DataAnnotations;

namespace CosmeticsStoreDB.Models
{
    public class ProductModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
