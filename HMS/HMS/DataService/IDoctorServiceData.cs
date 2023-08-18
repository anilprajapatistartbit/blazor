using HMS.Model;
using HMS.Model.DatabaseModel;
using HMS.Model.Response;
namespace HMS.DataService
{
    public interface IDoctorServiceData
    {

        Task<StatusResponse<Doctor>> Create(Doctor data);
        Task<IEnumerable<Doctor>> GetBySearch(int? id);
        Task<Doctor> GetDocById(string id);
        Task<StatusResponse<Doctor>> Update(Doctor data);

    }
}
