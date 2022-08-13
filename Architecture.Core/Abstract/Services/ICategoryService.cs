using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Core.Dtos;
using Architecture.Core.Entities;

namespace Architecture.Core.Abstract.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<CustomResponseDto<CategoryByIdWithProductsDto>> GetCategoryByIdWithProducts(int id);
    }
}
