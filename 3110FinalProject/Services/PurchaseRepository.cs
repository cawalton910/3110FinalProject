using _3110FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3110FinalProject.Services
{
    /// <summary>
    /// This repository handles creating, reading, and removing purchases from the database
    /// </summary>
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ICustomerRepository _customerRepo;

        public PurchaseRepository(ApplicationDbContext db,ICustomerRepository customerRepo)
        {
            _db = db;
            _customerRepo = customerRepo;
        }
        /// <summary>
        /// This method adds a purchase model to the database
        /// </summary>
        /// <param name="purchase">Purchase model</param>
        /// <param name="id">Customer ID</param>
        /// <returns></returns>
        public async Task<Purchase> CreateAsync(Purchase purchase, int id)
        {
            var customer = await _customerRepo.ReadAsync(id);
            if (customer != null)
            {
                purchase.Customer = customer;
                customer.Purchases.Add(purchase);
                _db.Purchases.Add(purchase);
                await _db.SaveChangesAsync();
            }
            return purchase;
        }
        /// <summary>
        /// This method return a list of all purchases from the database
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Purchase>> ReadAllAsync()
        {
            return await _db.Purchases
                .Include(p => p.Products)
                .ToListAsync();
        }
        /// <summary>
        /// This method returns one purchase from the database
        /// </summary>
        /// <param name="id">Purchase ID</param>
        /// <returns></returns>
        public async Task<Purchase?> ReadAsync(int id)
        {
            return await _db.Purchases
                .Include(p => p.Products)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        /// <summary>
        /// This method removes a purchase from the database
        /// </summary>
        /// <param name="purchaseId">Purchase ID</param>
        /// <returns></returns>
        public async Task RemoveAsync(int purchaseId)
        {
            var purchase = await ReadAsync(purchaseId);
            if (purchase != null)
            {
                _db.Purchases.Remove(purchase);
                await _db.SaveChangesAsync();
            }
        }

    }
}
