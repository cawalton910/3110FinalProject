using _3110FinalProject.Models.Entities;

namespace _3110FinalProject.Services
{
    /// <summary>
    /// Interface for the purchase repository
    /// </summary>
    public interface IPurchaseRepository
    {
        Task<ICollection<Purchase>> ReadAllAsync();
        Task<Purchase?> ReadAsync(int id);
        Task<Purchase> CreateAsync(Purchase purchase, int id);
        Task RemoveAsync(int purchaseId);
    }
}
