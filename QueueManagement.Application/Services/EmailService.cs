using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using QueueManagement.Application
    .Interfaces.Services;

namespace QueueManagement.Infrastructure
    .Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration
        _configuration;

    public EmailService(
        IConfiguration configuration)
    {
        _configuration =
            configuration;
    }

    public async Task SendEmailAsync(
        string toEmail,
        string subject,
        string body)
    {
        var smtpClient =
            new SmtpClient(
                _configuration["EmailSettings:Host"])
            {
                Port = int.Parse(
                _configuration[
                    "EmailSettings:Port"]!),

                Credentials =
                new NetworkCredential(
                    _configuration[
                        "EmailSettings:Email"],
                    _configuration[
                        "EmailSettings:Password"]),

                EnableSsl = true
            };

        var mailMessage =
            new MailMessage
            {
                From = new MailAddress(
                    _configuration[
                        "EmailSettings:Email"]!),

                Subject = subject,

                Body = body,

                IsBodyHtml = true
            };

        mailMessage.To.Add(toEmail);

        await smtpClient
            .SendMailAsync(mailMessage);
    }
}