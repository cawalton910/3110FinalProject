using System.ComponentModel;

namespace _3110FinalProject.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [DisplayName("Last Name")]
        public string? LastName { get; set; }
        [DisplayName("Purchase Orders")]
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    }
}
