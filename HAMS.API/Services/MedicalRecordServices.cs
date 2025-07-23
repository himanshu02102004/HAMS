using Hospital_Management.Database;
using Hospital_Management.DTO;
using Hospital_Management.Model;
using Hospital_Management.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    public class MedicalrecordServices : IMedicalRecordServices
    {

        private readonly Apicontext _apicontext;

        public MedicalrecordServices( Apicontext apicontext)
        {
         _apicontext = apicontext;   
        }




        public async Task<IEnumerable<MedicalRecord>> GetPrescriptionListAsync()
        {
            return await _apicontext.prescriptions
                      .Include(r => r.Patient)
                      .Include(r => r.doctor)
                      .ToListAsync();
        }


        public async Task<MedicalRecord> GetPrescriptionByIdAsync(int id)
        {
            return await _apicontext.prescriptions
                    .Include(r => r.Patient)
                    .Include(r => r.doctor)
                    .FirstOrDefaultAsync(r => r.ID==id);
        }





        public async Task<MedicalRecord> AddAsync(MedicalRecordDto dto)
        {

            var entity = new MedicalRecord
            {
                Patient_id = dto.Patient_id,
                Doctor_Id = dto.Doctor_Id,
                Notes = dto.Notes,
                FollowUpdate = dto.FollowUpdate,
                prescribe = dto.prescribe
            };



            _apicontext.AddAsync(entity);
                await _apicontext.SaveChangesAsync();

            var result = await _apicontext.prescriptions
                .Include(m => m.doctor)
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.ID == entity.ID);
                return result!;



        }

        public async Task<IEnumerable<MedicalRecord>> GetbyPatientid(int PatientId)
        {
            return await _apicontext.prescriptions
                     .Where(r=>r.Patient_id ==PatientId)
                    .Include(r => r.Patient)
                    .Include(r => r.doctor)
                    .ToListAsync();
        }

      
       
    }
}
