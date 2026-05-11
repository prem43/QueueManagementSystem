using AutoMapper;
using QueueManagement.Application.DTOs.Counter;
using QueueManagement.Application.Interfaces;
using QueueManagement.Application.Interfaces.Services;

namespace QueueManagement.Application.Services.Counter;

public class CounterService : ICounterService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public CounterService(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CounterDto>> GetAllAsync()
    {
        var counters = await _unitOfWork.Counters
            .FindAsync(c => !c.IsDeleted);

        return _mapper.Map<IEnumerable<CounterDto>>(counters);
    }

    public async Task<CounterDto?> GetByIdAsync(int id)
    {
        var counter = await _unitOfWork.Counters
            .GetByIdAsync(id);

        if (counter == null)
            return null;

        return _mapper.Map<CounterDto>(counter);
    }

    public async Task CreateAsync(CreateCounterDto dto)
    {
        var counter =
            _mapper.Map<Domain.Entities.Counter>(dto);

        await _unitOfWork.Counters.AddAsync(counter);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateCounterDto dto)
    {
        var counter = await _unitOfWork.Counters
            .GetByIdAsync(dto.Id);

        if (counter == null)
            throw new Exception("Counter not found.");

        counter.Name = dto.Name;
        counter.CounterNumber = dto.CounterNumber;
        counter.IsActive = dto.IsActive;
        counter.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Counters.Update(counter);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var counter = await _unitOfWork.Counters
            .GetByIdAsync(id);

        if (counter == null)
            throw new Exception("Counter not found.");

        counter.IsDeleted = true;

        _unitOfWork.Counters.Update(counter);

        await _unitOfWork.SaveChangesAsync();
    }
}