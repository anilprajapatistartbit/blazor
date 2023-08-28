using HMS.Model.DatabaseModel;
using HMS.Model.Response;
using HMS.Service.Interfaces;
using Radzen;
using System.Text.Json;

namespace HMS.DataService
{
    public class AppointmentServiceData : IAppointmentServiceData
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly ILogService _logService;
        #endregion

        #region Constructor
        public AppointmentServiceData(HttpClient httpClient,ILogService logService)
        {
            _httpClient = httpClient;
            _logService = logService;
        }
        #endregion

        #region Method

        public async Task<IEnumerable<Appointment>> GetAllById(string Id,string type)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Appointment>>("api/Appointment/GetListById/" + Id + "/" + type);

            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
        }

        public async Task<Appointment> GetById(string Id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Appointment>("api/Appointment/GetById/" + Id);

            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
        }


        public async Task<StatusResponse<Appointment>> Create(Appointment appointment)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                appointment.Id = guid.ToString();
                var response = await _httpClient.PostAsJsonAsync<Appointment>("api/Appointment", appointment);
                if (response.IsSuccessStatusCode)
                {
                    return new StatusResponse<Appointment>() { IsSuccess = true, Value = await response.ReadAsync<Appointment>(), Message = "Appointment created" };
                }
                else
                {
                    return new StatusResponse<Appointment>() { IsSuccess = false, Value = null, Message = "Failed to create appointment" };
                }
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
         
        }
        public async Task<StatusResponse<Appointment>> Update(Appointment appointment)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync<Appointment>("api/Appointment", appointment);
                if (response.IsSuccessStatusCode)
                {
                    return new StatusResponse<Appointment>() { IsSuccess = true, Value = await response.ReadAsync<Appointment>(), Message = "Appointment Updated" };
                }
                else
                {
                    return new StatusResponse<Appointment>() { IsSuccess = false, Value = null, Message = "Failed to update appointment" };
                }
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
           
        }
        public async Task<DashboardResponse<Appointment>> GetDashboardData(string Id, string type)
        {
            try
            {
                var appointment = await _httpClient.GetFromJsonAsync<IEnumerable<Appointment>>("api/Appointment/GetListById/" + Id + "/" + type);
                var patient = await _httpClient.GetFromJsonAsync<IEnumerable<Patient>>("api/Patient/GetByDocId/" + Id);
                var totalappointment = appointment.Where(s => s.Status.ToLower() != "cancelled").Count();
                var pendingappointment = appointment.Where(s => s.Status.ToLower() == "pending").Count();
                var totalpatient = patient.Count();
                var date = DateTime.Today;
                var todaysappointment = appointment.Where(s => s.Status.ToLower() != "cancelled" && s.AppointmentDate.Month == date.Month && s.AppointmentDate.Year == date.Year && s.AppointmentDate.Date == date.Date);
                return new DashboardResponse<Appointment>()
                {
                    TodaysAppointment = todaysappointment.Count(),
                    TotalPatient = totalpatient,
                    TotalAppointment = totalappointment,
                    PendingAppointment = pendingappointment,
                    list = todaysappointment
                };
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
           

        }
        public async Task<IEnumerable<Appointment>> GetAppointments(string Id, string type, DateTime date)
        {
            try
            {
                var appointment = await _httpClient.GetFromJsonAsync<IEnumerable<Appointment>>("api/Appointment/GetListById/" + Id + "/" + type);
                var todaysappointment = appointment.Where(s => s.Status.ToLower() != "cancelled" && s.AppointmentDate.Month == date.Month && s.AppointmentDate.Year == date.Year && s.AppointmentDate.Date == date.Date);
                return todaysappointment;
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
           
        }
        #endregion
    }
}
