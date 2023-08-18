using HMS.Model.DatabaseModel;
using HMS.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HMS.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        #region Fields
        private readonly IAppointmentService _appointmentService;
        #endregion

        #region Constructor
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        #endregion

        #region Method
        [HttpPost]
        public async Task<IActionResult> Post(Appointment appointment)
        {
            try
            {
                var res = await _appointmentService.Insert(appointment);
                return StatusCode(StatusCodes.Status200OK, res);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetListById/{Id}/{type}")]
        public async Task<IActionResult> GetAll(string Id, string type)
        {
            try
            {
                if (type.ToLower() == "doctor")
                {
                    var doc = await _appointmentService.GetByDoctorId(Id);
                    return StatusCode(StatusCodes.Status200OK, doc);
                }
                else
                {
                    var pat = await _appointmentService.GetByPatientId(Id);
                    return StatusCode(StatusCodes.Status200OK, pat);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            try
            {
                
                    var val = await _appointmentService.GetById(Id);
                    return StatusCode(StatusCodes.Status200OK, val);
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Appointment NewData)
        {
            try
            {
               // var NewData = JsonSerializer.Deserialize<Appointment>(rowdata);

                var val = await _appointmentService.GetById(NewData.Id);

                val.Status = NewData.Status;
                val.ReVisit = NewData.ReVisit;
                val.AppointmentDate = NewData.AppointmentDate;
                val.AppointmentTime = NewData.AppointmentTime;
                val.AnalysisReport = NewData.AnalysisReport;
                val.DoctorPrescription= NewData.DoctorPrescription;
                var res = await _appointmentService.Update(val);
                return StatusCode(StatusCodes.Status200OK, res);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
