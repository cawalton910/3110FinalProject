using _3110FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3110FinalProject.Services
{
    /// <summary>
    /// This repository handles CRUD operations for the customer database table
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// This method adds a customer model to the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return customer;
        }
        /// <summary>
        /// This method returns all customers in the database as a list including the related data
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Customer>> ReadAllAsync()
        {
            return await _db.Customers
                .Include(p => p.Purchases)
                .ToListAsync();
        }
        /// <summary>
        /// This method returns one customer in the database as a list including the related data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer?> ReadAsync(int id)
        {
            return await _db.Customers
                .Include(p => p.Purchases)
                .ThenInclude(p => p.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        /// <summary>
        /// This method updates a customers data in the databases
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This method removes a customer from the database
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
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
