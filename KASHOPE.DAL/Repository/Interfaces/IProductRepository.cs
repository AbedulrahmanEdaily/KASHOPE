using KASHOPE.DAL.Models;


namespace KASHOPE.DAL.Repository.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> FindByIdAsync(int id);
        Task<bool> DecreaseQuantityItemAsync(List<(int productId, int quantity)> products);
    }
}
