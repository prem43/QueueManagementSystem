using AutoMapper;
using QueueManagement.Application.DTOs.SubCategory;
using QueueManagement.Application.Interfaces;
using QueueManagement.Application.Interfaces.Services;

namespace QueueManagement.Application.Services.SubCategory;

public class SubCategoryService : ISubCategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public SubCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SubCategoryDto>> GetAllAsync()
    {
    //    var subCategories = await _unitOfWork.SubCategories
    //.FindAsync(s => !s.IsDeleted);
        var subCategories = await _unitOfWork.SubCategories.GetAllIncludingAsync(s => !s.IsDeleted, "Category");

        return subCategories.Select(s => new SubCategoryDto
        {
            Id = s.Id,
            CategoryId = s.CategoryId,
            CategoryName = s.Category.Name,
            Name = s.Name,
            Prefix = s.Prefix,
            IsActive = s.IsActive
        });
    }
    public async Task<IEnumerable<SubCategoryLookupDto>>GetByCategoryIdAsync(int categoryId)
    {
        var subCategories = await _unitOfWork.SubCategories
            .FindAsync(s =>
                s.CategoryId == categoryId
                && !s.IsDeleted
                && s.IsActive);

        return subCategories.Select(s =>
            new SubCategoryLookupDto
            {
                Id = s.Id,
                Name = s.Name,
                CategoryId = s.CategoryId
            });
    }
    public async Task CreateAsync(CreateSubCategoryDto dto)
    {
        var subCategory = _mapper.Map<Domain.Entities.SubCategory>(dto);

        await _unitOfWork.SubCategories.AddAsync(subCategory);

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task<SubCategoryDto?> GetByIdAsync(int id)
    {
        var subCategory = await _unitOfWork.SubCategories
            .GetByIdAsync(id);

        if (subCategory == null)
            return null;

        return _mapper.Map<SubCategoryDto>(subCategory);
    }
    public async Task UpdateAsync(UpdateSubCategoryDto dto)
    {
        var subCategory = await _unitOfWork.SubCategories
            .GetByIdAsync(dto.Id);

        if (subCategory == null)
            throw new Exception("SubCategory not found.");

        subCategory.Name = dto.Name;
        subCategory.Prefix = dto.Prefix;
        subCategory.CategoryId = dto.CategoryId;
        subCategory.IsActive = dto.IsActive;
        subCategory.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.SubCategories.Update(subCategory);

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var subCategory = await _unitOfWork.SubCategories
            .GetByIdAsync(id);

        if (subCategory == null)
            throw new Exception("SubCategory not found.");

        subCategory.IsDeleted = true;

        _unitOfWork.SubCategories.Update(subCategory);

        await _unitOfWork.SaveChangesAsync();
    }
}