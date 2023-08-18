using HMS.Model.DatabaseModel;
using HMS.Model.Response;
using HMS.Service.Implementations;
using HMS.Service.Interfaces;
using Radzen;
using System.Text.Json;

namespace HMS.DataService
{
    public class PatientServiceData : IPatientServiceData
    {
        #region Fields
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructor
        public PatientServiceData(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion

        #region Method

        public async Task<StatusResponse<Patient>> Create(Patient data)
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
        public async Task<IEnumerable<Patient>> GetByDocId(string Id)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Patient>>("api/Patient/GetByDocId/"+Id);
        }
        public async Task<Patient> GetById(string Id)
        {
            return await _httpClient.GetFromJsonAsync<Patient>("api/Patient/GetById/" + Id);
        }
        #endregion
    }
}
