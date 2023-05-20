using AutoMapper;
using CuMicroservice.Services.Catalog.Dtos;
using CuMicroservice.Services.Catalog.Models;

namespace CuMicroservice.Services.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course,CourseDto>().ReverseMap();
            CreateMap<Course,CourseCreateDto>().ReverseMap();
            CreateMap<Course,CourseUpdateDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Feature,FeatureDto>().ReverseMap();
        }
    }
}
