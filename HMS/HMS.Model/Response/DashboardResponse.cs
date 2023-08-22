using HMS.Model.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Response
{
    public class DashboardResponse<T> where T : class
    {
        public int TotalPatient { get; set; }
        public int TotalAppointment { get; set; }
        public int TodaysAppointment { get; set; }
        public int PendingAppointment { get; set; }
        public IEnumerable<T> list { get; set; }
    }
}
