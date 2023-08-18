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
        #endregion

        #region Constructor
        public DoctorController(IDoctorService doctorService)
        {
            _doctorservice = doctorService;
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
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
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
