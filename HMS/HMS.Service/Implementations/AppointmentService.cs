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
    public class AppointmentService :IAppointmentService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public AppointmentService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        #endregion

        #region Methods
        public async Task<Appointment> Insert(Appointment Appointment)
        {
            try
            {
                var result = await _unitOfWork.Appointments.AddData(Appointment);
                var resultcheck = await _unitOfWork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Appointment> Update(Appointment Appointment)
        {
            try
            {
                var result = await _unitOfWork.Appointments.EditData(Appointment);
                var resultcheck = await _unitOfWork.CompleteAsync();
                return await Task.Run(() => (resultcheck) ? result : null);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Appointment>> GetByPatientId(string Id)
        {
            try
            {
                return (await _unitOfWork.Appointments.GetAllByExpression(s=>s.PatientId==Id)).OrderByDescending(s=>s.AppointmentDate);
            }
            catch
            {
                throw;

            }
        }
        public async Task<IEnumerable<Appointment>> GetByDoctorId(string Id)
        {
            try
            {
                return (await _unitOfWork.Appointments.GetAllByExpression(s => s.DoctorId == Id)).OrderByDescending(s => s.AppointmentDate);
            }
            catch
            {
                throw;

            }
        }


        public async Task<Appointment> GetById(string Id)
        {
            try { return await _unitOfWork.Appointments.GetByExpression(s=>s.Id == Id); } catch { throw; }
        }
        #endregion
    
}
}
