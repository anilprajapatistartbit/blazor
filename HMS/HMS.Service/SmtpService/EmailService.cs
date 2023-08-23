using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace HMS.Service.SmtpService
{
    public  class EmailService : IEmailService
    {
        #region Fields
        EmailSetting _emailSetting;
        #endregion

        #region Constructor
        public EmailService(IOptions<EmailSetting> emailSetting)
        {
            _emailSetting = emailSetting.Value;
        }
        #endregion

        #region Methods
        public async Task<bool> SendEmailAsync(EmailMessage sendingemaildata)
        {
            try
            {
                await Task.Yield();
                string userState = "mailto" + sendingemaildata.ReceiversEmail;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_emailSetting.EmailId);
                mail.To.Add(sendingemaildata.ReceiversEmail);
                mail.Subject = sendingemaildata.Subject;
                mail.Body = sendingemaildata.Body;
                mail.IsBodyHtml = sendingemaildata.IsHtml;
                var smtp = new SmtpClient(_emailSetting.Host, _emailSetting.Port);
                smtp.EnableSsl = _emailSetting.UseSSL;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_emailSetting.Name, _emailSetting.Password);
                // smtp.SendAsync(mail, userState);
                smtp.Send(mail);
                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
