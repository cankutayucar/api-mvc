using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Core.Abstract.Repositories;
using Architecture.Core.Abstract.Services;
using Architecture.Core.Abstract.UnitOfWork;
using Architecture.Core.Dtos;
using Architecture.Core.Entities;
using AutoMapper;

namespace Architecture.Service.Abstract.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<CategoryByIdWithProductsDto>> GetCategoryByIdWithProducts(int id)
        {
            return CustomResponseDto<CategoryByIdWithProductsDto>.Success(200,
                _mapper.Map<CategoryByIdWithProductsDto>(
                    await _categoryRepository.GetCategoryByIdWithProducts(id)));
        }
    }
}
