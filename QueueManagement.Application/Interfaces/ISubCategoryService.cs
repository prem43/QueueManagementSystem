using QueueManagement.Application.DTOs.SubCategory;

namespace QueueManagement.Application.Interfaces.Services;

public interface ISubCategoryService
{
    Task<IEnumerable<SubCategoryDto>> GetAllAsync();

    Task CreateAsync(CreateSubCategoryDto dto);
    Task<SubCategoryDto?> GetByIdAsync(int id);

    Task UpdateAsync(UpdateSubCategoryDto dto);
    Task<IEnumerable<SubCategoryLookupDto>>GetByCategoryIdAsync(int categoryId);
    Task DeleteAsync(int id);
}