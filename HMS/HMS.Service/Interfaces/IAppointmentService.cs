using HMS.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment> Insert(Appointment Appointment);
        Task<Appointment> Update(Appointment Appointment);
        Task<Appointment> GetById(string Id);
        Task<IEnumerable<Appointment>> GetByPatientId(string Id);
        Task<IEnumerable<Appointment>> GetByDoctorId(string Id);
        Task<IEnumerable<Appointment>> GetAll();



    }
}
