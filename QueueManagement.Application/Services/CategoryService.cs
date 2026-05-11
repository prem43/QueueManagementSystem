using QueueManagement.Application.DTOs.Category;
using QueueManagement.Application.Interfaces;
using QueueManagement.Application.Interfaces.Services;
using QueueManagement.Domain.Entities;
using AutoMapper;
namespace QueueManagement.Application.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoryService( IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _unitOfWork.Categories
            .FindAsync(c => !c.IsDeleted);
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }
    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);

        if (category == null)
            return null;

        return _mapper.Map<CategoryDto>(category);
    }
    public async Task CreateAsync(CreateCategoryDto dto)
    {
        var category = _mapper.Map<Domain.Entities.Category>(dto);

        await _unitOfWork.Categories.AddAsync(category);

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task UpdateAsync(UpdateCategoryDto dto)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(dto.Id);

        if (category == null)
            throw new Exception("Category not found.");

        category.Name = dto.Name;
        category.Prefix = dto.Prefix;
        category.Description = dto.Description;
        category.IsActive = dto.IsActive;
        category.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Categories.Update(category);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Category not found.");

        category.IsDeleted = true;

        _unitOfWork.Categories.Update(category);

        await _unitOfWork.SaveChangesAsync();
    }
}