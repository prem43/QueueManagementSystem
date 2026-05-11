using QueueManagement.Application.DTOs.Counter;

namespace QueueManagement.Application.Interfaces.Services;

public interface ICounterService
{
    Task<IEnumerable<CounterDto>> GetAllAsync();

    Task<CounterDto?> GetByIdAsync(int id);

    Task CreateAsync(CreateCounterDto dto);

    Task UpdateAsync(UpdateCounterDto dto);

    Task DeleteAsync(int id);
}