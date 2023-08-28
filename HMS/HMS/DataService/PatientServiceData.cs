using HMS.Model.DatabaseModel;
using HMS.Model.Response;
using HMS.Service.Implementations;
using HMS.Service.Interfaces;
using Radzen;
using System.Net.Http.Json;
using System.Text.Json;

namespace HMS.DataService
{
    public class PatientServiceData : IPatientServiceData
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private ILogService _logService;
        #endregion

        #region Constructor
        public PatientServiceData(HttpClient httpClient, ILogService logService)
        {
            _httpClient = httpClient;
            _logService = logService;
        }
        #endregion

        #region Method

        public async Task<StatusResponse<Patient>> Create(Patient data)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                data.Id = guid.ToString();
                var response = await _httpClient.PostAsJsonAsync<Patient>("api/Patient", data);
                if (response.IsSuccessStatusCode)
                {
                    return new StatusResponse<Patient>() { IsSuccess = true, Value = await response.ReadAsync<Patient>(), Message = "Registration Successfull" };
                }
                else
                {
                    return new StatusResponse<Patient>() { IsSuccess = false, Value = null, Message = "Registration Failed" };
                }
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
           
        }
        public async Task<IEnumerable<Patient>> GetByDocId(string Id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Patient>>("api/Patient/GetByDocId/" + Id);

            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
        }
        public async Task<Patient> GetById(string Id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Patient>("api/Patient/GetById/" + Id);

            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
        }

        public async Task<StatusResponse<Patient>> Update(Patient patient)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync<Patient>("api/Patient/Update/" + patient.Id, patient);
                if (response.IsSuccessStatusCode)
                {
                    return new StatusResponse<Patient>() { IsSuccess = true, Value = await response.ReadAsync<Patient>(), Message = "Profile update successfully" };
                }
                else
                {
                    return new StatusResponse<Patient>() { IsSuccess = false, Value = null, Message = "Failed to update profile" };
                }
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
