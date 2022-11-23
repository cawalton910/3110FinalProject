using _3110FinalProject.Models.Entities;
using System.ComponentModel;

namespace _3110FinalProject.Models.ViewModels
{
    public class CustomerVM
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Purchase>? Purchases { get; set; }
        public Customer GetCustomerInstance()
        {
            return new Customer
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Purchases = Purchases
            };
        }
    }
}
