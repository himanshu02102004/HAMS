using Hospital_Management.Model;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;

namespace Hospital_Management.Services.IServices
{
    public interface IDoctorService
    {
        public Task<IEnumerable<Doctor>> GetallDoctor();
        Task<List<DoctorSchedule>> GetAllSchedules();
        public Task<Doctor> GetDoctorbyid(int id);
        public Task<Doctor> AddDoctor(Doctor doctor);
        public Task<DoctorSchedule> AddDoctorSchedule(DoctorSchedule schedule);
        public Task<bool>  DeleteDoctor(int id );
        public Task<bool> UpdateDoctor(Doctor doctor);
        public Task<bool> Markonleave(int id, bool IsonLeave);

       
    }
}
