using HMS.Model.DatabaseModel;
using HMS.Model.Response;
using HMS.Service.Interfaces;
using Radzen;
using System.Net.Http;
using System.Text.Json;

namespace HMS.DataService
{
    public class DepartmentServiceData : IDepatmentServiceData
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly ILogService _logService;
        #endregion

        #region Constructor
        public DepartmentServiceData(HttpClient httpClient,ILogService logService) 
        {
            _httpClient = httpClient;
            _logService = logService;
        }
        #endregion

        #region Method
        public async Task<Department[]> GetAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Department[]>("api/Department");

            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
        }

        public async Task<StatusResponse<Department>> Create(Department data)
        {
            try
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
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
          
        }
        #endregion
    }
}
