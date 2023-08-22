using HMS.Model.DatabaseModel;
using HMS.Service.Implementations;
using HMS.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HMS.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        #region Fields
        private readonly IPatientService _patientService;
        private readonly ILoginService _loginService;
        #endregion

        #region Constructor
        public PatientController(IPatientService patientService, ILoginService loginService)
        {
            _patientService = patientService;
            _loginService = loginService;
        }
        #endregion


        #region Actions
        [HttpGet("Count")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var res = await _patientService.TotalCount();
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetByDocId/{Id}")]
        public async Task<IActionResult> GetAllByDoc(string Id)
        {
            try
            {

                var login = await _loginService.GetById(Id);
                if(login != null && login.LoginType.ToLower()=="admin")
                {
                    var result = await _patientService.GetAll();
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                var res = await _patientService.GetAllByDocId(Id);
                return StatusCode(StatusCodes.Status200OK, res);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            try
            {

                var res = await _patientService.GetById(Id);
                return StatusCode(StatusCodes.Status200OK, res);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Patient data)
        {
            try
            {
                if (data != null)
                {
                    var res = await _patientService.Insert(data);
                    return StatusCode(StatusCodes.Status200OK, res);
                }

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Put(string Id, [FromBody] Patient data)
        {
            try
            {
                var pat = await _patientService.GetById(Id);
                var login = await _loginService.GetById(Id);
                if (pat == null)
                {
                    throw new Exception("Patient not found");
                }
                pat.FirstName = data.FirstName;
                pat.LastName = data.LastName;
                pat.PhoneNumber = data.PhoneNumber;
                pat.AlternatePhoneNumber = data.AlternatePhoneNumber;
                pat.AddressFirstLine = data.AddressFirstLine;
                pat.AddressSecondLine = data.AddressSecondLine;
                pat.City = data.City;
                pat.State = data.State;
                pat.Country = data.Country;
                pat.ZipCode = data.ZipCode;
                login.Name = $"{pat.FirstName} {pat.LastName}";
                var res = await _patientService.Update(pat);
                var l = await _loginService.Update(login);
                return StatusCode(StatusCodes.Status200OK, res);

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
