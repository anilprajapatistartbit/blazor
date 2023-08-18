using HMS.Infrastructure;
using HMS.Model.DatabaseModel;
using HMS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Implementations
{
    public class DoctorService : IDoctorService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public DoctorService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<Doctor> Insert(Doctor doctor)
        {
            try
            {
                var result = await _unitOfWork.Doctors.AddData(doctor);
                var resultcheck = await _unitOfWork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Doctor> Update(Doctor doctor)
        {
            try
            {
                var result = await _unitOfWork.Doctors.EditData(doctor);
                var resultcheck = await _unitOfWork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Doctor> GetById(string id)
        {
            try
            {
                return await _unitOfWork.Doctors.GetDataById(id);
            }
            catch
            {
                throw;
            
            }
        }
        public async Task<Doctor> GetByEmailId(string id)
        {
            try
            {
                return await _unitOfWork.Doctors.GetByExpression(s=>s.Email.ToLower()==id.ToLower());
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Doctor>> GetAll()
        {
            try { return await _unitOfWork.Doctors.GetData(); } catch { throw; }
        }
        public async Task<IEnumerable<Doctor>> GetByDepartment(int id)
        {
            try { return await _unitOfWork.Doctors.GetAllByExpression(s=>s.DepartmentId == id); } catch { throw; }
        }
        #endregion
    }
}
