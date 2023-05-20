using AutoMapper;
using CuMicroservice.Services.Catalog.Dtos;
using CuMicroservice.Services.Catalog.Models;
using CuMicroservice.Services.Catalog.Settings;
using CuMicroservice.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categories;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categories = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }


        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(await _categories.Find(x => true).ToListAsync()), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categories.InsertOneAsync(category);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 204);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string categoryId)
        {
            var category = await _categories.Find(x => x.Id == categoryId).FirstOrDefaultAsync();
            return category != null
                ? Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200)
                : Response<CategoryDto>.Fail($"{categoryId} bu id ye ait bir category bulunamadı", 404);
        }
    }
}
