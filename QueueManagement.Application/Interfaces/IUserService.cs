using QueueManagement.Application.DTOs.User;

namespace QueueManagement.Application.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();

    Task CreateAsync(CreateUserDto dto);
}