using _3110FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3110FinalProject.Services
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ICustomerRepository _customerRepo;

        public PurchaseRepository(ApplicationDbContext db,ICustomerRepository customerRepo)
        {
            _db = db;
            _customerRepo = customerRepo;
        }
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

        public async Task<ICollection<Purchase>> ReadAllAsync()
        {
            return await _db.Purchases
                .Include(p => p.Products)
                .ToListAsync();
        }

        public async Task<Purchase?> ReadAsync(int id)
        {
            return await _db.Purchases
                .Include(p => p.Products)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task RemoveAsync(int purchaseId)
        {
            var purchase = await ReadAsync(purchaseId);
            if (purchase != null)
            {
                _db.Purchases.Remove(purchase);
            }
        }

    }
}
