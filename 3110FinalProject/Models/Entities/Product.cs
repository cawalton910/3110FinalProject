using System.ComponentModel.DataAnnotations;

namespace _3110FinalProject.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [DisplayFormat(DataFormatString = "{0} lb")] //https://stackoverflow.com/questions/37668420/use-displayformat-within-a-custom-displaytemplate
        public float Weight { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public ICollection<ProductPurchase> ProductPurchases { get; set; } = new List<ProductPurchase>();
    }
}
