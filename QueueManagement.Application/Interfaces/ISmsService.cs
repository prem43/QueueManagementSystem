namespace QueueManagement.Application
    .Interfaces.Services;

public interface ISmsService
{
    Task SendSmsAsync(
        string mobileNumber,
        string message);
}