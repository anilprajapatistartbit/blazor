using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Interfaces
{
    public  interface ILogService
    {
        Task Info(string message);
        Task Error(Exception exception);
    }
}
