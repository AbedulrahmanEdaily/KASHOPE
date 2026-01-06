using KASHOPE.DAL.Models;


namespace KASHOPE.DAL.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> AddAsync(Product request);
    }
}
