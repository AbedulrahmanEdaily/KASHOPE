using KASHOPE.DAL.Models;


namespace KASHOPE.DAL.Repository.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>,IScopedRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> FindByIdAsync(int id);
        Task<bool> DecreaseQuantityItemAsync(List<(int productId, int quantity)> products);
        IQueryable<Product> Query();
    }
}
