using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Core.Abstract.Repositories;
using Architecture.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Repository.Concrete.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Category> GetCategoryByIdWithProducts(int id)
        {
            return await _context.Categories.Include(c => c.Products).Where(c => c.Id == id).SingleOrDefaultAsync();
        }
    }
}
