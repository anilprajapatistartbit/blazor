using HMS.Model.DatabaseModel;
using HMS.Model.Request;
using HMS.Model.Response;
using Radzen;
using System.Text.Json;

namespace HMS.DataService
{
    public class LoginServiceData : ILoginServiceData
    {
        #region Fields
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructor
        public LoginServiceData(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion

        #region Method

        public async Task<StatusResponse<Login>> Create(Login data)
        {


            var response = await _httpClient.PostAsJsonAsync<Login>("api/Auth/Create", data);
            if (response.IsSuccessStatusCode)
            {
                return new StatusResponse<Login>() { IsSuccess = true, Value = await response.ReadAsync<Login>(), Message = "Registration Successfull" };
            }
            else
            {
                return new StatusResponse<Login>() { IsSuccess = false, Value = null, Message = "Registration Failed" };
            }
        }

        public async Task<Login> Login(LoginModal value)
        {
            //var val = JsonSerializer.Serialize<LoginModal>(value);
            var response = await _httpClient.PostAsJsonAsync<LoginModal>("api/Auth/Login", value);
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadAsync<Login>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CheckEmail(string id)
        {
            var response = await _httpClient.GetFromJsonAsync<bool>("api/Auth/CheckId/" + id);

            return response;

        }
         public async Task<StatusResponse<Login>> PasswordLoginChange(PasswordChange passwordChange)
        {
            var response = await _httpClient.PostAsJsonAsync<PasswordChange>("api/Auth/Changepassword", passwordChange);
            if (response.IsSuccessStatusCode)
            {
                return new StatusResponse<Login>() { IsSuccess = true, Value = await response.ReadAsync<Login>(), Message = "Password change successfull" };
            }
            else
            {
                return new StatusResponse<Login>() { IsSuccess = false, Value = null, Message = "Failed to change password" };
            }
        }

        public async Task<IEnumerable<Login>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Login>>("api/Auth/GetAll");
        }

        public async Task<StatusResponse<Login>> LoginIsActive(string id, bool status)
        {
            var val = await _httpClient.PutAsJsonAsync<bool>("api/Auth/Status/" + id, status);
            var message = status ? "enable" : "disable";
            if (val.IsSuccessStatusCode)
            {
                return new StatusResponse<Login>() { IsSuccess = true, Value = await val.ReadAsync<Login>(), Message = $"User successfully {message}d" };
            }
            else
            {
                return new StatusResponse<Login>() { IsSuccess = false, Value = null, Message = $"Failed to {message} user" };
            }
        }
        #endregion
    }
}
