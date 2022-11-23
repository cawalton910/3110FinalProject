using System.ComponentModel;

namespace _3110FinalProject.Models.Entities
{
    public class Purchase
    {
        [DisplayName("Purchase Order #")]
        public int Id { get; set; }
        [DisplayName("Date/Time")]
        public DateTime Date { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Notes { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<ProductPurchase> Products { get; set; } = new List<ProductPurchase>();
    }
}
