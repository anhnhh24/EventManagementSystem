using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

public class EmailService
{
    public async Task SendConfirmationEmailAsync(string toEmail, string fullName, string subject, string content)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("elearningfptedu@gmail.com"));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = content
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync("elearningfptedu@gmail.com", "qqwh ygmr uopg togx");
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
