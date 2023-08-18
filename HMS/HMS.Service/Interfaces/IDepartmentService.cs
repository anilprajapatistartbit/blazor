using HMS.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> Insert(Department doctor);
        Task<IEnumerable<Department>> GetAll();
    }
}
