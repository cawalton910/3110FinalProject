using _3110FinalProject.Models.Entities;
using _3110FinalProject.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace _3110FinalProject.Services
{
    /// <summary>
    /// This class handles CRUD operations for products
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// This method adds a product model to the database
        /// </summary>
        /// <param name="product">Product Model</param>
        /// <returns></returns>
        public async Task<Product> CreateAsync(Product product)
        {
             _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }
        /// <summary>
        /// This method returns a list of all products from the database
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Product>> ReadAllAsync()
        {
            return await _db.Products
                .Include(p => p.ProductPurchases)
                .ToListAsync();
        }
        /// <summary>
        /// This method returns one product from the database
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns></returns>
        public async Task<Product?> ReadAsync(int id)
        {
            return await _db.Products
                .Include(p => p.ProductPurchases)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        /// <summary>
        /// This method removes a product from the database
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <returns></returns>
        public async Task RemoveAsync(int productId)
        {
            var product = await ReadAsync(productId);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// This method updates a products data in the database
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="product">Product Model</param>
        /// <returns></returns>
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
