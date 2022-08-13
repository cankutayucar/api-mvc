using Architecture.Core.Abstract.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Repository.Concrete.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public Task<List<Product>> GetProductsWithCategory()
        {
            return _context.Products.Include(p => p.Category).ToListAsync();
        }
    }
}
