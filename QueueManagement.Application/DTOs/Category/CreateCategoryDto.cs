using System.ComponentModel.DataAnnotations;

namespace QueueManagement.Application.DTOs.Category;

public class CreateCategoryDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Prefix { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Description { get; set; }
}