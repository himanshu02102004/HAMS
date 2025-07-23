using Hospital_Management.Configuration;
using Hospital_Management.Model;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Database
{
    public class Apicontext : DbContext
    {
        public Apicontext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<UserApplication> Users { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Auditlog> Auditlogs { get; set; }
   
        public DbSet<EmailSetting> emailSettings { get; set; }
        public DbSet<MedicalRecord> prescriptions { get; set; }
        public DbSet<DoctorSchedule> doctorSchedules { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalRecordConfiguration());


         

        }
    }
}
