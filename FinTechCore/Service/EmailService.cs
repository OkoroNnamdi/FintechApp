using FinTech.DB;
using FinTechCore.IService;
using FinTechCore.Utilities;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendEmailMassageAsync(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }
        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("HMS", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = string.Format("<p>{0}</p>", message.Content) };
            return emailMessage;
        }
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    //await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    //await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    //await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    //client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
