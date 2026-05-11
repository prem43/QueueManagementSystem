using Microsoft.AspNetCore.Identity;
using QueueManagement.Application.DTOs.User;
using QueueManagement.Application.Interfaces.Services;
using QueueManagement.Domain.Entities;

namespace QueueManagement.Application.Services.User;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser>
        _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = _userManager.Users.ToList();

        var result = new List<UserDto>();

        foreach (var user in users)
        {
            var roles =
                await _userManager.GetRolesAsync(user);

            result.Add(new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName!,
                Email = user.Email!,
                Role = roles.FirstOrDefault() ?? ""
            });
        }

        return result;
    }

    public async Task CreateAsync(CreateUserDto dto)
    {
        var user = new ApplicationUser
        {
            FullName = dto.FullName,

            UserName = dto.UserName,

            Email = dto.Email,

            CounterId = dto.CounterId
        };

        var result = await _userManager
            .CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new Exception(
                string.Join(", ",
                result.Errors.Select(e => e.Description)));
        }

        await _userManager.AddToRoleAsync(
            user,
            dto.Role);
    }
}