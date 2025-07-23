using Hospital_Management.DTO;
using Hospital_Management.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Services.IServices
{
    public interface IPatientServices
    {

        public Task<List<Patient>> GetAllPatient ();
        public  Task<Patient> GetPatientById (int id );
        public Task<Patient> AddPatient(Patient patient);
        public  Task<Patient> UpdatePatient (int id, PatientUpdateDto patientUpdateDto);
       public Task<Patient> DeletePatient (int id);
       public Task<bool> PartialUpdate(int id, [FromBody] PatientPartialUpdateDto patientPartialUpdateDto);
       public Task<IEnumerable<Patient>> SearchPatient(string query);
    }
}
