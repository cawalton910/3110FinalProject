using _3110FinalProject.Models.Entities;
using System.ComponentModel;

namespace _3110FinalProject.Models.ViewModels
{
    public class PurchaseVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Delivery Address")]
        public string? DeliveryAddress { get; set; }
        public string? Notes { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<ProductPurchase>? Products { get; set; }
        public Purchase GetPurchaseInstance()
        {
            return new Purchase
            {
                Id = 0,
                Date = Date,
                Notes = Notes,
                DeliveryAddress = DeliveryAddress,
                CustomerId = CustomerId,
                Customer = Customer,
                Products = Products
            };
        }
    }
}
