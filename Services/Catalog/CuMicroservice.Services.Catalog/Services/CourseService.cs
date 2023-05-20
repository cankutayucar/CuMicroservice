using AutoMapper;
using CuMicroservice.Services.Catalog.Dtos;
using CuMicroservice.Services.Catalog.Models;
using CuMicroservice.Services.Catalog.Settings;
using CuMicroservice.Shared.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courses;
        private readonly IMongoCollection<Category> _categories;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courses = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categories = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }


        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courses.Find(x => true).ToListAsync();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categories.Find(Builders<Category>.Filter.Eq(x => x.Id, course.CategoryId)).FirstAsync();
                }
                return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
            }
            else
            {
                courses = new List<Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }



        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courses.Find(Builders<Course>.Filter.Eq(x => x.Id, id)).FirstAsync();
            if (course == null) return Response<CourseDto>.Fail($"{id} bu id ye bağlı bir course bulunamadı", 404);
            course.Category = await _categories.Find(Builders<Category>.Filter.Eq(x => x.Id, course.CategoryId)).FirstAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }


        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courses.Find(Builders<Course>.Filter.Eq(x => x.UserId, userId)).ToListAsync();
            if (!courses.Any()) courses = new List<Course>();
            foreach (var course in courses)
            {
                course.Category = await _categories.Find(Builders<Category>.Filter.Eq(x => x.Id, course.CategoryId)).FirstAsync();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.CreatedTime = DateTime.Now;
            await _courses.InsertOneAsync(newCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updatedCourse = _mapper.Map<Course>(courseUpdateDto);
            var course = await _courses.FindOneAndReplaceAsync(Builders<Course>.Filter.Eq(x => x.Id, courseUpdateDto.Id), updatedCourse);
            if (course == null) { return Response<NoContent>.Fail("course bulunamadı", 404); }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courses.DeleteOneAsync(Builders<Course>.Filter.Eq(x => x.Id, id));
            if (result.DeletedCount > 0) return Response<NoContent>.Success(204);
            return Response<NoContent>.Fail($"{id} id sine bağlı bir course bulunamadı", 204);
        }
    }
}
