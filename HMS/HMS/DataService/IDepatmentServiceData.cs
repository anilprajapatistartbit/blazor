using HMS.Model.DatabaseModel;
using HMS.Model.Response;

namespace HMS.DataService
{
    public interface IDepatmentServiceData
    {

        Task<Department[]> GetAll();
        Task<StatusResponse<Department>> Create(Department data);
    }
}
