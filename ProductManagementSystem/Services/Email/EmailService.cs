using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using ProductManagementSystem.DTO.Email;

namespace ProductManagementSystem.Services.Email;

public class EmailService : IEmailService
{
    public void SendEmail()
    {
        var emailRequest = new EmailRequest();

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(""));
        email.To.Add(MailboxAddress.Parse(emailRequest.To));
        email.Subject = emailRequest.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = emailRequest.Body };

        using var smtp = new SmtpClient();
        smtp.Connect("", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("", "");
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
