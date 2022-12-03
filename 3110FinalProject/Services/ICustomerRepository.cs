using _3110FinalProject.Models.Entities;

namespace _3110FinalProject.Services
{
    /// <summary>
    /// Interface for the customer repository
    /// </summary>
    public interface ICustomerRepository
    {
        Task<ICollection<Customer>> ReadAllAsync();
        Task<Customer?> ReadAsync(int id);
        Task<Customer> CreateAsync(Customer customer);
        Task RemoveAsync(int customerId);
        Task UpdateAsync(int cusotmerId, Customer customer);
    }
}
