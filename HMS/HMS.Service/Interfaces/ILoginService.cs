using HMS.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Interfaces
{
    public interface ILoginService
    {
        Task<Login> Insert(Login u);
        Task<Login> GetById(string id);
        Task<Login> CheckEmailId(string id);
        Task<Login> GetByIdWithType(string id,string type);
        Task<Login> Update(Login doctor);
        Task<IEnumerable<Login>> GetAll();
        Task<Login> SentForgotPasswrdOtp(string id);
        Task<Login> ChangePassword(Login login , string otp);


    }
}
