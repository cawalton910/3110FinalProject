using _3110FinalProject.Models.Entities;
using _3110FinalProject.Models.ViewModels;

namespace _3110FinalProject.Services
{
    /// <summary>
    /// Interface for the purchaseproduct repository
    /// </summary>
    public interface IPurchaseProductRepository
    {
        Task<ProductPurchase?> CreateAsync(int purchaseId, int productId, int quantity);
        Task RemoveAsync(int purchaseId, int productPurchaseId);
        Task<ProductPurchase?> ReadAsync(int id);
    }
}
