using System.ComponentModel.DataAnnotations;

namespace QueueManagement.Application.DTOs.SubCategory;

public class CreateSubCategoryDto
{
    [Required]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Prefix { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}