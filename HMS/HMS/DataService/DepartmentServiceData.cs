using HMS.Model.DatabaseModel;
using HMS.Model.Response;
using Radzen;
using System.Net.Http;
using System.Text.Json;

namespace HMS.DataService
{
    public class DepartmentServiceData : IDepatmentServiceData
    {
        #region Fields
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructor
        public DepartmentServiceData(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
        #endregion

        #region Method
        public async Task<Department[]> GetAll()
        {
            return  await _httpClient.GetFromJsonAsync<Department[]>("api/Department");
        }

        public async Task<StatusResponse<Department>> Create(Department data)
        {
            var response = await _httpClient.PostAsJsonAsync<Department>("api/Department", data);
            if (response.IsSuccessStatusCode)
            {
                return new StatusResponse<Department>() { IsSuccess = true, Value = await response.ReadAsync<Department>(), Message = "Department Added" };
            }
            else
            {
                return new StatusResponse<Department>() { IsSuccess = false, Value = null, Message = "Failed to Add Department" };
            }
        }
        #endregion
    }
}
