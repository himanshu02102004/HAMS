using Hospital_Management.DTO;
using Hospital_Management.Model;

namespace Hospital_Management.Services.IServices
{
    public interface IMedicalRecordServices
    {

        Task<IEnumerable<MedicalRecord>> GetPrescriptionListAsync();
        Task<MedicalRecord> GetPrescriptionByIdAsync(int  id);

        Task<IEnumerable<MedicalRecord>> GetbyPatientid(int PatientId);// FILTERED LIST

        Task<MedicalRecord> AddAsync(MedicalRecordDto dto);

    }
}
