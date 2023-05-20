using CuMicroservice.Services.Catalog.Dtos;
using CuMicroservice.Services.Catalog.Services;
using CuMicroservice.Shared.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _categoryService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return CreateActionResultInstance(await _categoryService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            return CreateActionResultInstance(await _categoryService.CreateAsync(categoryDto));
        }
    }
}
