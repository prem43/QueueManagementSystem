namespace QueueManagement.Application.DTOs.SubCategory;

public class SubCategoryDto
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Prefix { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}