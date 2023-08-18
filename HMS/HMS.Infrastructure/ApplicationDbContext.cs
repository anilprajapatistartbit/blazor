using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HMS.Model.DatabaseModel;
namespace HMS.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        #endregion

        #region Fields
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        #endregion
    }
}
