using QueueManagement.Application.DTOs.Category;

namespace QueueManagement.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();

    Task<CategoryDto?> GetByIdAsync(int id);

    Task CreateAsync(CreateCategoryDto dto);

    Task UpdateAsync(UpdateCategoryDto dto);

    Task DeleteAsync(int id);
}