using HMS.Model.DatabaseModel;
using HMS.Model.Response;

namespace HMS.DataService
{
    public interface IAppointmentServiceData
    {
        Task<IEnumerable<Appointment>> GetAllById(string Id,string type);
        Task<Appointment> GetById(string Id);
        Task<StatusResponse<Appointment>> Create(Appointment appointment);
        Task<StatusResponse<Appointment>> Update(Appointment appointment);
        Task<DashboardResponse<Appointment>> GetDashboardData(string Id,string type);
        Task<IEnumerable<Appointment>> GetAppointments(string Id, string type,DateTime date);
    }
}
