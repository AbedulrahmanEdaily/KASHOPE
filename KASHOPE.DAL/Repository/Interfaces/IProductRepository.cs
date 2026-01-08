using KASHOPE.DAL.Models;


namespace KASHOPE.DAL.Repository.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<Product?> FindByIdAsync(int id);
    }
}
