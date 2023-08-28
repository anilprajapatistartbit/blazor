using HMS.Model.DatabaseModel;
using HMS.Model.Request;
using HMS.Model.Response;
using HMS.Service.Interfaces;
using Radzen;
using System.Text.Json;

namespace HMS.DataService
{
    public class LoginServiceData : ILoginServiceData
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly ILogService _logService;
        #endregion

        #region Constructor
        public LoginServiceData(HttpClient httpClient,ILogService logService)
        {
            _httpClient = httpClient;
            _logService = logService;
        }
        #endregion

        #region Method

        public async Task<StatusResponse<Login>> Create(Login data)
        {

            try
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
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
         
        }

        public async Task<Login> Login(LoginModal value)
        {
            try
            {
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
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
          
        }

        public async Task<bool> CheckEmail(string id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<bool>("api/Auth/CheckId/" + id);

                return response;
            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return false;
            }
           

        }
         public async Task<StatusResponse<Login>> PasswordLoginChange(PasswordChange passwordChange)
        {
            try
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
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
           
        }

        public async Task<IEnumerable<Login>> GetAll()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Login>>("api/Auth/GetAll");

            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
        }

        public async Task<StatusResponse<Login>> LoginIsActive(string id, bool status)
        {
            try
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
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
          
        }

        public async Task<StatusResponse<Login>> SendOtp(string email)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<StatusResponse<Login>>("api/Auth/Sendotp/" + email);

            }
            catch (Exception ex)
            {
                await _logService.Error(ex);
                return null;
            }
        }
        public async Task<StatusResponse<Login>> ChangePassword(ForgotPwd forgot)

        {
            try
            {
                forgot.CurrentPassword = forgot.NewPassword;
                var val = await _httpClient.PutAsJsonAsync<ForgotPwd>("api/Auth/ForgotPassword", forgot);

                if (val.IsSuccessStatusCode)
                {
                    return new StatusResponse<Login>() { IsSuccess = true, Value = await val.ReadAsync<Login>(), Message = $"Password chnage successfully" };
                }
                else
                {
                    return new StatusResponse<Login>() { IsSuccess = false, Value = null, Message = $"Failed to change password" };
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
