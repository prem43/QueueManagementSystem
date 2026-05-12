using QueueManagement.Application
    .Interfaces.Services;

namespace QueueManagement.Infrastructure
    .Services;

public class SmsService : ISmsService
{
    public async Task SendSmsAsync(
        string mobileNumber,
        string message)
    {
        // DEMO SMS

        await Task.Delay(500);

        Console.WriteLine(
            $"SMS Sent To: {mobileNumber}");

        Console.WriteLine(message);
    }
}