using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.DatabaseModel;
namespace HMS.Service.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> Insert(Doctor doctor);
        Task<Doctor> Update(Doctor doctor);

        Task<Doctor> GetById(string id);
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor> GetByEmailId(string id);
        Task<IEnumerable<Doctor>> GetByDepartment(int id);
    }
}
