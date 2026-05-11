using AutoMapper;
using QueueManagement.Application.DTOs.Category;
using QueueManagement.Domain.Entities;
using QueueManagement.Application.DTOs.SubCategory;
using QueueManagement.Application.DTOs.Token;
using QueueManagement.Application.DTOs.Counter;
namespace QueueManagement.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
        CreateMap<SubCategory, CreateSubCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<SubCategory, UpdateSubCategoryDto>().ReverseMap();
        CreateMap<Token, TokenDto>().ReverseMap();

        CreateMap<Token, GenerateTokenDto>().ReverseMap();
        CreateMap<Counter, CounterDto>().ReverseMap();

        CreateMap<Counter, CreateCounterDto>().ReverseMap();

        CreateMap<Counter, UpdateCounterDto>().ReverseMap();
    }
}