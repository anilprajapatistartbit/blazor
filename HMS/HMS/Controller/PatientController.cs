using HMS.Model.DatabaseModel;
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
        #endregion

        #region Constructor
        public PatientController(IPatientService patientService)
        { 
           _patientService= patientService;
        }
        #endregion


        #region Actions
        [HttpGet("GetByDocId/{Id}")]
        public async Task<IActionResult> GetAllByDoc(string Id)
        {
            try
            {
               
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
        #endregion
    }
}
