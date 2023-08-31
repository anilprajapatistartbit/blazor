using HMS.Infrastructure;
using HMS.Model.DatabaseModel;
using HMS.Model.Request;
using HMS.Service.Interfaces;
using HMS.Service.SmtpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Implementations
{
    public class LoginService : ILoginService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private static string OTP = "";
        #endregion

        #region Constructor
        public LoginService(IUnitOfWork UnitOfWork , IEmailService emailService)
        {
            _unitOfWork = UnitOfWork;
            _emailService = emailService;
        }
        #endregion

        #region Methods
        public async Task<Login> Insert(Login doctor)
        {
            try
            {
                var result = await _unitOfWork.Logins.AddData(doctor);
                var resultcheck = await _unitOfWork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Login> Update(Login doctor)
        {
            try
            {
                var result = await _unitOfWork.Logins.EditData(doctor);
                var resultcheck = await _unitOfWork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Login> GetById(string id)
        {
            try
            {
                
                return await _unitOfWork.Logins.GetByExpression(s=>s.Id == id);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Login> CheckEmailId(string id)
        {
            try
            {
                return await _unitOfWork.Logins.GetByExpression(s=>s.Email.ToLower() == id.ToLower());   
                
            }
            catch
            {
                throw;
            }
        }
        public async Task<Login> GetByIdWithType(string id, string type)
        {
            try
            {
                return await _unitOfWork.Logins.GetByExpression(s => s.Email.ToLower() == id.ToLower() && s.LoginType.ToLower()==type.ToLower());

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Login>> GetAll()
        {
            try
            {
                return (await _unitOfWork.Logins.GetData()).OrderBy(x => x.Name);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Login> ChangePassword(Login login ,string Otp)
        {
            try
            {
                if (Otp == OTP)
                {
                    var result = await _unitOfWork.Logins.EditData(login);
                    var resultcheck = await _unitOfWork.CompleteAsync();
                    return await Task.Run(() => (resultcheck) ? result : null);
                }

                return null;
               
            }
            catch
            {
                throw;
            }
            
            
        }

        public async Task<Login> SentForgotPasswrdOtp(string email)
        {
            try
            {
                var data = await _unitOfWork.Logins.GetByExpression(s => s.Email.ToLower() == email.ToLower());
                if (data == null)
                {
                    return null;
                }
                var otp = GeneratePassword().ToString();
                OTP = otp;
                var body = "<!DOCTYPE html><html><head><style>   body {font - family: Arial, sans-serif;    background-color: #f4f4f4;    margin: 0;    padding: 0;  }  .container {max - width: 600px;    margin: 0 auto;    padding: 20px;    background-color: #ffffff;    border-radius: 5px;    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);  }  .header {text - align: center;    margin-bottom: 20px;  }  .message {padding: 20px;    background-color: #f9f9f9;    border-radius: 5px;  }  .button {display: inline-block;    padding: 10px 20px;    background-color: #007bff;    color: #ffffff;    text-decoration: none;    border-radius: 5px;  }</style></head><body><div class="+"container"+">  <div class="+"header"+">    <h1>Your One Time Password</h1>  </div>  <div class="+"message"+">    <p>Hello <strong>" + data.Name + ",</strong></p>   <p>Here is your  Email:</p><p><strong>" + data.Email + "</strong></p>   <p>Here is your  OTP:</p>    <p><strong>" + OTP + "</strong></p> <p>If you have any questions or concerns, feel free to contact our support team.</p>    <p>Thank you!</p>  </div>  <div class="+"footer"+">    <p>This is an automated email. Please do not reply.</p>  </div></div></body></html>";
                var res = await _emailService.SendEmailAsync(new EmailMessage() { Body = body, IsHtml = true, ReceiversEmail = data.Email, Subject = "Forgot Password" });
                return data;
            }
            catch
            {
                throw;
            }
        }
        private string GeneratePassword()
        {
            int PasswordLength = 6;
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";


            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string IDString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < PasswordLength; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;
            }
            return NewPassword;
        }
        #endregion
    }
   
}
