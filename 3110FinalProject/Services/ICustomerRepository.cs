using _3110FinalProject.Models.Entities;

namespace _3110FinalProject.Services
{
    public interface ICustomerRepository
    {
        Task<ICollection<Customer>> ReadAllAsync();
        Task<Customer?> ReadAsync(int id);
        Task<Customer> CreateAsync(Customer customer);
        Task RemoveAsync(int customerId);
        Task UpdateAsync(int cusotmerId, Customer customer);
    }
}
