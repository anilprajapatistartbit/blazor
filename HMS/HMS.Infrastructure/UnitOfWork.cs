using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.DatabaseModel;
namespace HMS.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private ApplicationDbContext _context;
        private GenericRepository<Login> _logins;
        private GenericRepository<Department> _departments;
        private GenericRepository<Patient> _patients;
        private GenericRepository<Doctor> _doctors;
        private GenericRepository<Appointment> _appointments;
        #endregion

        #region Constructors
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public IGenericRepository<Login> Logins
        {
            get
            {
                return _logins ??
                    (_logins = new GenericRepository<Login>(_context));
            }
        }
        public IGenericRepository<Department> Departments
        {
            get
            {
                return _departments ??
                    (_departments = new GenericRepository<Department>(_context));
            }
        }
        public IGenericRepository<Patient> Patients
        {
            get
            {
                return _patients ??
                    (_patients = new GenericRepository<Patient>(_context));
            }
        }
        public IGenericRepository<Doctor> Doctors
        {
            get
            {
                return _doctors ??
                    (_doctors = new GenericRepository<Doctor>(_context));
            }
        }

        public IGenericRepository<Appointment> Appointments
        {
            get
            {
                return _appointments ??
                    (_appointments = new GenericRepository<Appointment>(_context));
            }
        }
        public async Task<bool> CompleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}
