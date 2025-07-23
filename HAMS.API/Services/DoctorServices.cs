using Hospital_Management.Database;
using Hospital_Management.Model;
using Hospital_Management.Models;
using Hospital_Management.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    public class DoctorServices : IDoctorService
    {

        private readonly Apicontext _apicontext;
        public DoctorServices( Apicontext apicontext)
        {
         _apicontext = apicontext;   
        }


        public async Task<IEnumerable<Doctor>> GetallDoctor()
        {
            var doctors = await _apicontext.doctors
       .Include(d => d.Department)
       .ToListAsync();

            return doctors;

        }

        public async Task<List<DoctorSchedule>> GetAllSchedules()
        {
            return await _apicontext.doctorSchedules.ToListAsync();
        }







        public async Task<Doctor> GetDoctorbyid(int id)
        {
            return await _apicontext.doctors.Include(e => e.Department)
                                            .FirstOrDefaultAsync(e => e.Doctor_Id == id);
        }



        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            await _apicontext.doctors.AddAsync(doctor);
            await _apicontext.SaveChangesAsync();
            return doctor;
        }




        public async Task<DoctorSchedule> AddDoctorSchedule(DoctorSchedule schedule)
        {
            await _apicontext.doctorSchedules.AddAsync(schedule);
            await _apicontext.SaveChangesAsync();
            return schedule;
        }




        public async Task<bool> UpdateDoctor(Doctor doctor)
        {
            var existing = await _apicontext.doctors.FindAsync(doctor);
            if (existing == null) {
                return false;
                    }
            existing.Doctor_Name=doctor.Doctor_Name;
            existing.Doctor_Description=doctor.Doctor_Description;
            existing.Doctor_specialization=doctor.Doctor_specialization;
            await _apicontext.SaveChangesAsync();
            return true;

        }




        public async Task<bool> DeleteDoctor(int id)
        {
            var found = await _apicontext.doctors.FindAsync(id);
            if(found == null)
            {
                return  false;
            }

            _apicontext.doctors.Remove(found);
            await _apicontext.SaveChangesAsync();

            return true;
              
        }

    

        public async Task<bool> Markonleave(int id, bool IsonLeave)
        {
            var find = await _apicontext.doctors.FindAsync(id);
                if(find == null)
            {
                return false;
            }

            find.IsonLeave = IsonLeave;
          await _apicontext.SaveChangesAsync();
            return true;

        }

      
    }
}
