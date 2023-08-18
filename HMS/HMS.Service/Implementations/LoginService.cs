using HMS.Infrastructure;
using HMS.Model.DatabaseModel;
using HMS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Implementations
{
    public class LoginService : ILoginService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public LoginService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<Login> Insert(Login doctor)
        {
            try
            {
                var result = await _unitOfWork.Logins.AddData(doctor);
                var resultcheck = await _unitOfWork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Login> Update(Login doctor)
        {
            try
            {
                var result = await _unitOfWork.Logins.EditData(doctor);
                var resultcheck = await _unitOfWork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Login> GetById(string id)
        {
            try
            {
                
                return await _unitOfWork.Logins.GetByExpression(s=>s.Id == id);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Login> CheckEmailId(string id)
        {
            try
            {
                return await _unitOfWork.Logins.GetByExpression(s=>s.Email.ToLower() == id.ToLower());   
                
            }
            catch
            {
                throw;
            }
        }
        public async Task<Login> GetByIdWithType(string id, string type)
        {
            try
            {
                return await _unitOfWork.Logins.GetByExpression(s => s.Email.ToLower() == id.ToLower() && s.LoginType.ToLower()==type.ToLower());

            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
