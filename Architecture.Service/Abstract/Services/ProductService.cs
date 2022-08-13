using Architecture.Core.Abstract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Core.Abstract.Repositories;
using Architecture.Core.Abstract.UnitOfWork;
using Architecture.Core.Dtos;
using Architecture.Core.Entities;
using AutoMapper;

namespace Architecture.Service.Abstract.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IGenericRepository<Product> repository, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var dto = _mapper.Map<List<ProductWithCategoryDto>>(await _productRepository.GetProductsWithCategory());
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, dto);
        }
    }
}
