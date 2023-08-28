using HMS.Model.DatabaseModel;
using HMS.Model.Response;
using HMS.Service.Interfaces;
using Newtonsoft.Json.Linq;
using Radzen;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
namespace HMS.DataService
{
    public class DoctorServiceData :IDoctorServiceData
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly ILogService _logService;
        #endregion

        #region Constructor
        public DoctorServiceData(HttpClient httpClient,ILogService logService)
        {
            _httpClient = httpClient;
            _logService = logService;
        }
        #endregion

        #region Method
      
        public async Task<StatusResponse<Doctor>> Create(Doctor data)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                data.Id = guid.ToString();
                var response = await _httpClient.PostAsJsonAsync<Doctor>("api/Doctor/Create", data);
                if (response.IsSuccessStatusCode)
                {
                    return new StatusResponse<Doctor>() { IsSuccess = true, Value = await response.ReadAsync<Doctor>(), Message = "Registration Successfull" };
                }
                else
                {
                    return new StatusResponse<Doctor>() { IsSuccess = false, Value = null, Message = "Registration Failed" };
                }
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
         
        }

        public async Task<IEnumerable<Doctor>> GetBySearch(int? id)
        {
            try
            {
                if (id == 0)
                {
                    var response = await _httpClient.GetFromJsonAsync<IEnumerable<Doctor>>("api/Doctor");
                    return response;
                }
                else
                {
                    var res = await _httpClient.GetFromJsonAsync<IEnumerable<Doctor>>("api/Doctor/" + id);
                    return res;
                }
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
           

        }
        public async Task<Doctor> GetDocById(string id)
        {
            try
            {
                var res = await _httpClient.GetFromJsonAsync<Doctor>("api/Doctor/GetDoc/" + id);
                return res;
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
          
        }
        public async Task<StatusResponse<Doctor>> Update(Doctor data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync<Doctor>("api/Doctor/Update/" + data.Id, data);
                if (response.IsSuccessStatusCode)
                {
                    return new StatusResponse<Doctor>() { IsSuccess = true, Value = await response.ReadAsync<Doctor>(), Message = "Profile update successfulyy" };
                }
                else
                {
                    return new StatusResponse<Doctor>() { IsSuccess = false, Value = null, Message = "Failed to update profile" };
                }
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
           
        }
        public async Task<DashboardResponse<Doctor>> GetDashboardById(string id,string type)
        {
            try
            {
                var doctor = await _httpClient.GetFromJsonAsync<int>("api/Doctor/Count");
                var patient = await _httpClient.GetFromJsonAsync<int>("api/Patient/Count");
                var list = await _httpClient.GetFromJsonAsync<IEnumerable<Doctor>>("api/Doctor");
                var appointment = await _httpClient.GetFromJsonAsync<IEnumerable<Appointment>>($"api/Appointment/GetListById/{id}/{type}");
                var pendingappointment = appointment.Where(s => s.Status.ToLower() == "pending").Count();
                var date = DateTime.Today;
                var todaysappointment = appointment.Where(s => s.Status.ToLower() != "cancelled" && s.AppointmentDate.Month == date.Month && s.AppointmentDate.Year == date.Year && s.AppointmentDate.Date == date.Date).Count();
                return new DashboardResponse<Doctor>() { TotalPatient = patient, TodaysAppointment = todaysappointment, list = list, PendingAppointment = pendingappointment, TotalAppointment = doctor };

            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
           
        }
        public async Task<IEnumerable<Doctor>> GetAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Doctor>>("api/Doctor");

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
