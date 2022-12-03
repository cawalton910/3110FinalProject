using _3110FinalProject.Models.Entities;

namespace _3110FinalProject.Services
{
    /// <summary>
    /// Interface for the product repository
    /// </summary>
    public interface IProductRepository
    {
        Task<ICollection<Product>> ReadAllAsync();
        Task<Product?> ReadAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task RemoveAsync(int productId);
        Task UpdateAsync(int productId, Product product);
    }
}
