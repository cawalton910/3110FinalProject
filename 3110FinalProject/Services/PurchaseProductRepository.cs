using _3110FinalProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3110FinalProject.Services
{
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

        public async Task RemoveAsync(int purchaseId, int productPurchaseId)
        {
            var purchase = await _purchaseRepo.ReadAsync(purchaseId);
            var productPurchase = await ReadAsync(productPurchaseId);
            var product = productPurchase!.Product;
            purchase!.Products.Remove(productPurchase);
            product!.ProductPurchases.Remove(productPurchase);
            await _db.SaveChangesAsync();
        }
        public async Task<ProductPurchase?> ReadAsync(int id)
        {
            return await _db.ProductPurchase
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
