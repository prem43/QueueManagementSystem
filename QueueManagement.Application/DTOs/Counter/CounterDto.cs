namespace QueueManagement.Application.DTOs.Counter;

public class CounterDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CounterNumber { get; set; }

    public bool IsActive { get; set; }
}