namespace PlanSphere.Core.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(string recipient, string subject, string message);
}