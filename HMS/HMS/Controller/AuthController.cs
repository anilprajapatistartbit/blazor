using HMS.Helper;
using HMS.Model.DatabaseModel;
using HMS.Model.Request;
using HMS.Model.Response;
using HMS.Service.Interfaces;
using HMS.Service.SmtpService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Claims;
using System.Text.Json;
using System.Xml.Linq;

namespace HMS.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Fields
        private readonly ILoginService _loginservice;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructor
        public AuthController(ILoginService loginService, IDoctorService doctorService, IPatientService patientService,IEmailService emailService)
        {
            _loginservice = loginService;
            _doctorService = doctorService;
            _patientService = patientService;
            _emailService = emailService;
        }
        #endregion

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var val = await _loginservice.GetAll();
                return StatusCode(StatusCodes.Status200OK, val);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("CheckId/{id}")]
        public async Task<IActionResult> CheckEmail(string id)
        {
            try
            {

                var check = await _loginservice.CheckEmailId(id);
                if(check != null)
                {
                    return StatusCode(StatusCodes.Status200OK, true);
                }
                return StatusCode(StatusCodes.Status200OK, false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #region Actions
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Login data)
        {
            try
            {
                if (data.LoginType.ToLower() == "doctor")
                {
                    var password = Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-"));
                    byte[] salt;
                    var hash = PasswordHelper.HashPasword(password, out salt);
                    var hexstring = Convert.ToHexString(salt);
                    data.HashPassword = hash;
                    data.SaltPassword = hexstring;

                    var body = "<!DOCTYPE html><html><head><style>   body {font - family: Arial, sans-serif;    background-color: #f4f4f4;    margin: 0;    padding: 0;  }  .container {max - width: 600px;    margin: 0 auto;    padding: 20px;    background-color: #ffffff;    border-radius: 5px;    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);  }  .header {text - align: center;    margin-bottom: 20px;  }  .message {padding: 20px;    background-color: #f9f9f9;    border-radius: 5px;  }  .button {display: inline-block;    padding: 10px 20px;    background-color: #007bff;    color: #ffffff;    text-decoration: none;    border-radius: 5px;  }</style></head><body><div class=\"container\">  <div class=\"header\">    <h1>Your Login Password</h1>  </div>  <div class=\"message\">    <p>Hello <strong>" + data.Name + ",</strong></p>   <p>Here is your  Email:</p><p><strong>" + data.Email + "</strong></p>   <p>Here is your  password:</p>    <p><strong>" + password + "</strong></p>    <p>Please make sure to change your password after logging in for security reasons.</p>    <p>If you have any questions or concerns, feel free to contact our support team.</p>    <p>Thank you!</p>  </div>  <div class=\"footer\">    <p>This is an automated email. Please do not reply.</p>  </div></div></body></html>";
                    var email = await _emailService.SendEmailAsync(new EmailMessage() { Body = body, IsHtml = true, ReceiversEmail = data.Email, Subject = "Login Password" });
                }
                var res = await _loginservice.Insert(data);
                return StatusCode(StatusCodes.Status200OK, res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("Changepassword")]
        public async Task<IActionResult> Post([FromBody] PasswordChange data)
        {
            try
            {
                var res = await _loginservice.GetById(data.UserId);
                if(res == null)
                {
                    throw new Exception("User not found");
                }
                byte[] salt = Convert.FromHexString(res.SaltPassword);
                var checkoldpassword = PasswordHelper.VerifyPassword(data.CurrentPassword, res.HashPassword, salt);
                if (!checkoldpassword)
                {
                    throw new Exception("Invalid current password");
                }
                byte[] newsalt;
                var hash = PasswordHelper.HashPasword(data.NewPassword, out salt);
                var hexstring = Convert.ToHexString(salt);
                res.HashPassword = hash;
                res.SaltPassword = hexstring;
                var val = await _loginservice.Update(res);
                return StatusCode(StatusCodes.Status200OK, val);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModal data)
        {

            try
            {
                
                var login = await _loginservice.GetByIdWithType(data.UserName,data.LoginType);
                if (login == null)
                {
                    throw new Exception("User not found");
                }

                return StatusCode(StatusCodes.Status200OK, login);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("Status/{id}")]
        public async Task<IActionResult> LoginUpdate(string id, [FromBody] bool status)
        {
            try
            {

                var login = await _loginservice.GetById(id);

                if (login == null)
                {
                    throw new Exception("User not found");
                }
                login.IsActive= status;

                var res = await _loginservice.Update(login);
                return StatusCode(StatusCodes.Status200OK, res);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Sendotp/{email}")]
        public async Task<IActionResult> SendOtp(string email)
        {
            try
            {

                var check = await _loginservice.SentForgotPasswrdOtp(email);
                var data = new StatusResponse<Login>() { IsSuccess = true, Value = check, Message = $"Otp successfully send on {email}" };
                if (check == null)
                {
                    data.IsSuccess = false;
                    data.Value = new Login();
                    data.Message = "User not found";
                   
                }
                return StatusCode(StatusCodes.Status200OK, data);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("ForgotPassword")]
        public async Task<IActionResult> SendFPOtp(ForgotPwd pwd)
        {
            try
            {

                var res = await _loginservice.GetById(pwd.UserId);
                if (res == null)
                {
                    throw new Exception("User not found");
                }
                byte[] salt;
                var hash = PasswordHelper.HashPasword(pwd.NewPassword, out salt);
                var hexstring = Convert.ToHexString(salt);
                res.HashPassword = hash;
                res.SaltPassword = hexstring;
                var val = await _loginservice.ChangePassword(res,pwd.Otp);
                if (val == null)
                {
                    throw new Exception("Invalide Otp");
                }
                return StatusCode(StatusCodes.Status200OK, val);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion
    }
}
