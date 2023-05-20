using CuMicroservice.Services.Catalog.Dtos;
using CuMicroservice.Services.Catalog.Models;
using CuMicroservice.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string categoryId);
    }
}
