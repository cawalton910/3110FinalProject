using _3110FinalProject.Models.Entities;
using _3110FinalProject.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace _3110FinalProject.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Product> CreateAsync(Product product)
        {
             _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<ICollection<Product>> ReadAllAsync()
        {
            return await _db.Products
                .Include(p => p.ProductPurchases)
                .ToListAsync();
        }

        public async Task<Product?> ReadAsync(int id)
        {
            return await _db.Products
                .Include(p => p.ProductPurchases)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task RemoveAsync(int productId)
        {
            var product = await ReadAsync(productId);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int productId, Product product)
        {
            var prod = await ReadAsync(productId);
            if (prod != null)
            {
                prod.Name = product.Name;
                prod.Weight = product.Weight;
                prod.Price = product.Price;
                await _db.SaveChangesAsync();
            };
        }
    }
}
