using _3110FinalProject.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace _3110FinalProject.Models.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Weight { get; set; }
        public double Price { get; set; }
        public ICollection<ProductPurchase>? ProductPurchases { get; set; } = new List<ProductPurchase>();
        public Product GetPurchaseInstance()
        {
            return new Product
            {
                Id = Id,
                Name = Name,
                Weight = Weight,
                Price = Price,
                ProductPurchases = ProductPurchases
            };
        }
    }
}
