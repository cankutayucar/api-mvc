using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Architecture.Core.Abstract.Repositories;
using Architecture.Core.Abstract.Services;
using Architecture.Core.Abstract.UnitOfWork;
using Architecture.Core.Dtos;
using Architecture.Core.Entities;
using Architecture.Service.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Architecture.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;
            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                //_memoryCache.Set(CacheProductKey, _repository.GetAll().ToList());
                _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
            }
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) not found");
            }
            return Task.FromResult(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(expression.Compile());
            return Task.FromResult(product != null);

        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entities;
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200,
                _mapper.Map<List<ProductWithCategoryDto>>(_memoryCache.Get<List<Product>>(CacheProductKey))));
        }


        public async Task CacheAllProductsAsync()
        {
            await _memoryCache.Set(CacheProductKey, _repository.GetAll().ToListAsync());
        }
    }
}
