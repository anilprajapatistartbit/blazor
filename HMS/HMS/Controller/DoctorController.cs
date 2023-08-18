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
    public class DoctorController : ControllerBase
    {
        #region Fields
        private readonly IDoctorService _doctorservice;
        private readonly ILoginService _loginService;
        #endregion

        #region Constructor
        public DoctorController(IDoctorService doctorService, ILoginService loginService)
        {
            _doctorservice = doctorService;
            _loginService = loginService;
        }
        #endregion


        #region Actions
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            try
            {
                var res = await _doctorservice.GetByDepartment(id);
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _doctorservice.GetAll();
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Doctor data)
        {
            try
            {

                if (data != null)
                {
                    var res = await _doctorservice.Insert(data);
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

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Doctor data)
        {
            try
            {
                var doc = await _doctorservice.GetById(id);
                var login = await _loginService.GetById(id);
                if (doc == null)
                {
                    throw new Exception("Doctor not found");
                }
                doc.FirstName = data.FirstName;
                doc.LastName = data.LastName;
                doc.MiddleName = data.MiddleName;
                doc.PhoneNumber = data.PhoneNumber;
                doc.AlternatePhoneNumber = data.AlternatePhoneNumber;
                doc.AddressFirstLine = data.AddressFirstLine;
                doc.AddressSecondLine = data.AddressSecondLine;
                doc.City = data.City;
                doc.State = data.State;
                doc.Country = data.Country;
                doc.ZipCode = data.ZipCode;
                doc.Dob = data.Dob;
                doc.Experience = data.Experience;
                doc.Qualification = data.Qualification;
                doc.DepartmentId = data.DepartmentId;
                doc.Speciality = data.Speciality;
                var middle = (doc.MiddleName == null || doc.MiddleName == "") ? " " : " "+doc.MiddleName + " ";
                login.Name = $"{doc.FirstName}{middle}{doc.LastName}";
                var res = await _doctorservice.Update(doc);
                var l = await _loginService.Update(login);
                return StatusCode(StatusCodes.Status200OK, res);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetDoc/{id}")]
        public async Task<IActionResult> GetDoc(string id)
        {
            try
            {
                var res = await _doctorservice.GetById(id);
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
