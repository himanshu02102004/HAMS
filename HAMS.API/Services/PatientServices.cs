  using Hospital_Management.Database;
using Hospital_Management.DTO;
using Hospital_Management.Model;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    


    public class PatientServices : IPatientServices
    {
        private readonly Apicontext apicontext;

        public PatientServices(Apicontext _apicontext)
        {
            apicontext = _apicontext;
        }


        public  async Task<List<Patient>> GetAllPatient() {
            var patient= await apicontext.Patients.ToListAsync();
            if(patient == null)
            {
                return null;
            }
            return patient;

            }
        public async Task<Patient> GetPatientById(int id) {
            return await apicontext.Patients.FindAsync(id);

        }
        public async Task<Patient> AddPatient(Patient patient) {

            apicontext.Patients.Add(patient);
            await apicontext.SaveChangesAsync();
            return patient;
            

        }
        public async Task<Patient> UpdatePatient (int id, PatientUpdateDto patientUpdateDto){
            var update = await apicontext.Patients.FindAsync(id);
            if(update == null)
            {
                return null;


            }

              update.Patient_name = patientUpdateDto.Patient_name;
            update.Patient_description=patientUpdateDto.Patient_Description;
           update.patient_phoneNo= patientUpdateDto.Patient_phoneNo;
            update.Email = patientUpdateDto.Patient_email;
           
            await apicontext.SaveChangesAsync();
            return update;
        }


        public async Task<Patient> DeletePatient(int id) { 
           var pat= await apicontext.Patients.FindAsync(id);
            if(pat == null)
            {
                return null;
            }

            apicontext.Patients.Remove(pat);
            await apicontext.SaveChangesAsync();
            return pat;


        
        }




        public async Task<IEnumerable<Patient>> SearchPatient(string query) {

            return await apicontext.Patients
                .Where(p => p.Patient_name.ToLower().Contains(query.ToLower())
                || p.patient_phoneNo.Contains(query))
             
            .ToListAsync();
            
        
        }
        public async Task<bool> PartialUpdate(int id, PatientPartialUpdateDto updatepatient)
        {
            var existing = await apicontext.Patients.FindAsync(id);
            if (existing == null)
                return false;

            if (updatepatient.Patient_name != null)
                existing.Patient_name = updatepatient.Patient_name;

            if (updatepatient.Patient_description != null)
                existing.Patient_description = updatepatient.Patient_description;

            if (updatepatient.Patient_phoneNo != null)
                existing.patient_phoneNo = updatepatient.Patient_phoneNo;

            await apicontext.SaveChangesAsync();
            return true;
        }



    }
}
