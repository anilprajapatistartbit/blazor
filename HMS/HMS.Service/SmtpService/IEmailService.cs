using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.SmtpService
{
    public interface IEmailService
    {
            Task<bool> SendEmailAsync(EmailMessage email);
    }
}
