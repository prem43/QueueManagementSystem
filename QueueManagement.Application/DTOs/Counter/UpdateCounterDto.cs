using System.ComponentModel.DataAnnotations;

namespace QueueManagement.Application.DTOs.Counter;

public class UpdateCounterDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 1000)]
    public int CounterNumber { get; set; }

    public bool IsActive { get; set; }
}