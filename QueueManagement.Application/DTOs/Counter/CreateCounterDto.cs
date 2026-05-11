using System.ComponentModel.DataAnnotations;

namespace QueueManagement.Application.DTOs.Counter;

public class CreateCounterDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 1000)]
    public int CounterNumber { get; set; }
}