using Architecture.API.Filters;
using Architecture.Core.Abstract.Services;
using Architecture.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int id)
        {
            return base.CreaateActionResult(await _categoryService.GetCategoryByIdWithProducts(id));
        }
    }
}
