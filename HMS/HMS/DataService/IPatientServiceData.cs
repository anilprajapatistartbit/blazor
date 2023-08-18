using HMS.Model.DatabaseModel;
using HMS.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace HMS.DataService
{
    public interface IPatientServiceData
    {
        Task<StatusResponse<Patient>> Create(Patient patient);
        Task<IEnumerable<Patient>> GetByDocId(string Id);
        [HttpGet("GetByDocId/{Id}")]
        Task<Patient> GetById(string Id);
    }
}
