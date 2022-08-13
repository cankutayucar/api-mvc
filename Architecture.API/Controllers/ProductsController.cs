using Architecture.Core.Abstract.Services;
using Architecture.Core.Dtos;
using Architecture.Core.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IService<Product> service, IProductService productService)
        {
            _mapper = mapper;
            _service = service;
            _productService = productService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return base.CreaateActionResult(await _productService.GetProductsWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = _mapper.Map<List<ProductDto>>(await _service.GetAllAsync());
            return base.CreaateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = _mapper.Map<ProductDto>(await _service.GetByIdAsync(id));
            return base.CreaateActionResult(CustomResponseDto<ProductDto>.Success(200, product));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            return base.CreaateActionResult(
                CustomResponseDto<ProductDto>.Success(201, _mapper.Map<ProductDto>(product)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto updateDto)
        {
           await _service.UpdateAsync(_mapper.Map<Product>(updateDto));
           return CreaateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoveAsync(await _service.GetByIdAsync(id));
            return base.CreaateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
