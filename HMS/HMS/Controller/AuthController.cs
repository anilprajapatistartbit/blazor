using HMS.Helper;
using HMS.Model.DatabaseModel;
using HMS.Model.Request;
using HMS.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        #endregion

        #region Constructor
        public AuthController(ILoginService loginService, IDoctorService doctorService, IPatientService patientService)
        {
            _loginservice = loginService;
            _doctorService = doctorService;
            _patientService = patientService;
        }
        #endregion

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
        #endregion
    }
}
