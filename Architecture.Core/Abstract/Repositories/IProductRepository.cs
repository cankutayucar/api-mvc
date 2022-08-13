using Architecture.Core.Entities;

namespace Architecture.Core.Abstract.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
    }
}
