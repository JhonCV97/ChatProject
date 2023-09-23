using Application.DTOs.Email;
using Application.Interfaces.Email;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        void IEmailService.SendEmail(EmailDto request)
        {
            MailAddress from = new MailAddress("ucarchitech@gmail.com");
            MailAddress to = new MailAddress(request.To);
            MailMessage message = new MailMessage(from, to);
            message.Subject = request.Subject;
            message.Body = request.Body;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("ucarchitech@gmail.com", "aofueutwysayhkia");
            client.EnableSsl = true;
            Console.WriteLine("Sending an email message to {0} at {1} by using the SMTP host={2}.",
                to.User, to.Host, client.Host);
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }
}
