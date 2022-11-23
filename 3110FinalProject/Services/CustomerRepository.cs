using _3110FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3110FinalProject.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return customer;
        }

        public async Task<ICollection<Customer>> ReadAllAsync()
        {
            return await _db.Customers
                .Include(p => p.Purchases)
                .ToListAsync();
        }

        public async Task<Customer?> ReadAsync(int id)
        {
            return await _db.Customers
                .Include(p => p.Purchases)
                .ThenInclude(p => p.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task UpdateAsync(int customerId, Customer customer)
        {
            var cust = await ReadAsync(customerId);
            if (cust != null)
            {
                cust.FirstName = customer.FirstName;
                cust.LastName = customer.LastName;
                await _db.SaveChangesAsync();
            };

        }

        public async Task RemoveAsync(int customerId)
        {
            var customer = await ReadAsync(customerId);
            if (customer != null)
            {
                _db.Customers.Remove(customer);
                await _db.SaveChangesAsync();
            }
        }

    }
}
