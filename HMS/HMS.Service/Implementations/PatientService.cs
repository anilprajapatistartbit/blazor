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
    public class PatientService : IPatientService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public PatientService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<Patient> Insert(Patient p)
        {
            try
            {
                var result = await _unitOfWork.Patients.AddData(p);
                var resultcheck = await _unitOfWork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Patient> GetByEmailId(string id)
        {
            try
            {
                return await _unitOfWork.Patients.GetByExpression(s => s.Email.ToLower() == id.ToLower());
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Patient>> GetAllByDocId(string Id)
        {
            try
            {
                var appointment = await _unitOfWork.Appointments.GetAllByExpression(s => s.DoctorId == Id);
                var group = appointment.DistinctBy(s => s.PatientId);
                List<Patient> list = new();
                foreach(var data in group)
                {
                    list.Add(data.Patient);
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Patient> GetById(string Id)
        {
            try
            {
                return await _unitOfWork.Patients.GetByExpression(s=>s.Id == Id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Patient> Update(Patient patient)
        {
            try
            {
                var result = await _unitOfWork.Patients.EditData(patient);
                var resultcheck = await _unitOfWork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalCount()
        {
            try { return await _unitOfWork.Patients.CountAll(); }catch { throw; }
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            try
            {
                return await _unitOfWork.Patients.GetData();
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
