using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISendGridEmail
    {
        public Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
