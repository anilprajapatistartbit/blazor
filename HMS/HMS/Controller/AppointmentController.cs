using HMS.Model.DatabaseModel;
using HMS.Service.Interfaces;
using HMS.Service.SmtpService;
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
        private readonly ILogService _logService;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructor
        public AppointmentController(IAppointmentService appointmentService, ILogService logService,IEmailService emailService)
        {
            _appointmentService = appointmentService;
            _logService = logService;   
            _emailService = emailService;
        }
        #endregion

        #region Method
        [HttpPost]
        public async Task<IActionResult> Post(Appointment appointment)
        {
            try
            {
                var res = await _appointmentService.Insert(appointment);
               await _logService.Info("Appoinment added");
                return StatusCode(StatusCodes.Status200OK, res);

            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
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
                else if (type.ToLower() == "admin")
                {
                    var adm = await _appointmentService.GetAll();
                    return StatusCode(StatusCodes.Status200OK, adm);
                }
                else
                {
                    var pat = await _appointmentService.GetByPatientId(Id);
                    return StatusCode(StatusCodes.Status200OK, pat);
                }
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
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
                await _logService.Error(ex);
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
                if (val.Status.ToLower() == "confirmed")
                {
                    var body = "<!DOCTYPE html><html><head><style>  body {    font-family: Arial, sans-serif;    margin: 0;    padding: 0;    background-color: #f4f4f4;  }  .container {    max-width: 600px;    margin: 0 auto;    padding: 20px;    background-color: #ffffff;    border-radius: 5px;    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);  }  .header {    background-color: #007bff;    color: #ffffff;    text-align: center;    padding: 10px;    border-top-left-radius: 5px;    border-top-right-radius: 5px;  }  .content {    padding: 20px;  }</style></head><body>  <div class="+"container"+">    <div class="+"header"+">      <h1>Appointment Confirmation</h1>    </div>    <div class="+"content"+">      <p>Dear "+res.Patient.FirstName+" "+res.Patient.LastName+",</p>      <p>Your appointment has been successfully confirmed.</p>      <p><strong>Appointment Details:</strong></p>      <ul>        <li><strong>Date:</strong> "+res.AppointmentDate.ToString("dddd,dd MMMM yyyy")+"</li>        <li><strong>Time:</strong> "+res.AppointmentTime.Value.ToString("t")+"</li>        </ul>      <p>If you have any questions or need to reschedule, please contact us.</p>      <p>Thank you for choosing our services.</p></div>  </div></body></html>";
                    var em = await _emailService.SendEmailAsync(new EmailMessage() { Body = body, IsHtml = true, ReceiversEmail = res.Patient.Email, Subject = "Appointment Confirmation" });

                }
                
                await _logService.Info("Apointment updated");
                return StatusCode(StatusCodes.Status200OK, res);

            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
