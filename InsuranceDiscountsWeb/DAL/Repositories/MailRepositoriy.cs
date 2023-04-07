using DAL.Interfaces;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public class MailRepositoriy : IMailRepository
    {
        private IConfiguration configuration;

        public MailRepositoriy(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    
        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = configuration["SendGridApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ihor.panasenko1@nure.ua", "Ihor Panasenko");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
       }
    }
}
