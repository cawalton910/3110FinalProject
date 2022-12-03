using _3110FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3110FinalProject.Services
{
    /// <summary>
    /// This repository handles creating, reading, and removing the relationship between a product and purchase
    /// </summary>
    public class PurchaseProductRepository : IPurchaseProductRepository
    {
        private readonly IPurchaseRepository _purchaseRepo;
        private readonly IProductRepository _productRepo;
        private readonly ApplicationDbContext _db;

        public PurchaseProductRepository(ApplicationDbContext db, IPurchaseRepository purchaseRepo, IProductRepository productRepo)
        {
            _purchaseRepo = purchaseRepo;
            _productRepo = productRepo;
            _db = db;
        }
        /// <summary>
        /// This creates a relationship between a product and a purchase. 
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<ProductPurchase?> CreateAsync(int purchaseId, int productId, int quantity)
        {
            var purchase = await _purchaseRepo.ReadAsync(purchaseId);
            if (purchase == null)
            {
                return null;
            }
            var product = await _productRepo.ReadAsync(productId);
            if (product == null)
            {
                return null;
            }
            var Purchase = new ProductPurchase()
            {
                Product = product,
                Purchase = purchase,
                Quantity = quantity

            };
            Purchase.Purchase = purchase;
            Purchase.Product = product;

            purchase.Products.Add(Purchase);
            product.ProductPurchases.Add(Purchase);
            await _db.SaveChangesAsync();
            return Purchase;
        }
        /// <summary>
        /// This removes a relationship between a product and a purchase
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <param name="productPurchaseId"></param>
        /// <returns></returns>
        public async Task RemoveAsync(int purchaseId, int productPurchaseId)
        {
            var purchase = await _purchaseRepo.ReadAsync(purchaseId);
            var productPurchase = await ReadAsync(productPurchaseId);
            var product = productPurchase!.Product;
            purchase!.Products.Remove(productPurchase);
            product!.ProductPurchases.Remove(productPurchase);
            await _db.SaveChangesAsync();
        }
        /// <summary>
        /// This returns a productpurchase from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductPurchase?> ReadAsync(int id)
        {
            return await _db.ProductPurchase
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
