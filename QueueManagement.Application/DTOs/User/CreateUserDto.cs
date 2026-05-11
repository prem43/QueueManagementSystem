using System.ComponentModel.DataAnnotations;

namespace QueueManagement.Application.DTOs.User;

public class CreateUserDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = "Staff";
    public int? CounterId { get; set; }
}