using System.Net;
using System.Net.Mail;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Interfaces.Services;

namespace PlanSphere.Core.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string recipient, string subject, string message)
    {
        var mail = Environment.GetEnvironmentVariable(EnvironmentConstants.EmailUsr)!;
        var psw = Environment.GetEnvironmentVariable(EnvironmentConstants.EmailPsw)!;

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(mail, psw)
        };

        var email = new MailMessage(
            from: mail,
            to: recipient,
            subject,
            message
        );

        email.IsBodyHtml = true;

        await client.SendMailAsync(email);
    }
}