using HMS.Model.DatabaseModel;
using HMS.Model.Request;
using HMS.Model.Response;

namespace HMS.DataService
{
    public interface ILoginServiceData
    {
        Task<StatusResponse<Login>> Create(Login login);
        Task<Login> Login(LoginModal value);
        Task<bool> CheckEmail(string id);
        Task<StatusResponse<Login>> PasswordLoginChange(PasswordChange passwordChange);
        Task<IEnumerable<Login>> GetAll();
        Task<StatusResponse<Login>> LoginIsActive(string id,bool status);
    }
}
