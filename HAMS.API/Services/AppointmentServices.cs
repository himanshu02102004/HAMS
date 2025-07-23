using Hospital_Management.Database;
using Hospital_Management.Model;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Hospital_Management.Services
{
    public class AppointmentServices : IAppointmentServices 
    {
        private readonly Apicontext _apiconext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailServices _emailservices;
        public AppointmentServices(Apicontext apicontext,IHttpContextAccessor httpContextAccessor, IEmailServices emailServices)
        {
            _apiconext = apicontext;
            _httpContextAccessor = httpContextAccessor;
            _emailservices = emailServices;
        }



   



        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _apiconext.appointments.Include(e => e.patient)
                                            .Include(e => e.Doctor)
                                            .ThenInclude(e => e.Department)
                                            .ToListAsync();


        }



        public async Task<Appointment> GetAppointmentbyID(int id)
        {
            return await _apiconext.appointments.Include(e => e.patient)
                                                .Include(e => e.Doctor)
                                                .ThenInclude(e => e.Department)
                                                .FirstOrDefaultAsync(e => id == id);


        }


      

        public async Task<Appointment?> BookAppointment(Appointment appointment)
        {
            var doc = await _apiconext.doctors.FindAsync(appointment.Doctor_Id);
            if (doc == null || doc.IsonLeave) return null;

         
            var slots = await GetAvailablesLotsAsync(appointment.Doctor_Id, appointment.Appointment_Date.Date);

            if (slots.Count == 0)
            {
                
                DateTime nextDay = appointment.Appointment_Date.AddDays(1);
                slots = await GetAvailablesLotsAsync(appointment.Doctor_Id, nextDay);

                if (slots.Count == 0)
                    return null;

                appointment.Appointment_Date = slots.First(); 
            }
            else
            {
                // Assign first available TODAY
                appointment.Appointment_Date = slots.First();
            }

            appointment.status = "Scheduled";

            _apiconext.appointments.Add(appointment);
            await _apiconext.SaveChangesAsync(); 

            // Log booking
            var currentUser = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "Unknown User";
            var currentRole = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "Role")?.Value ?? "Unknown Role";

            await LogActionAsync("Book Appointment", currentRole,
                $"Booked appointment for patient {appointment.Patient_id} with doctor {appointment.Doctor_Id} at {appointment.Appointment_Date} by {currentUser}");

            // Email
            var patient = await _apiconext.Patients.FindAsync(appointment.Patient_id);
            var doctor1 = await _apiconext.doctors.FindAsync(appointment.Doctor_Id);

            await _emailservices.SendEmailAsync(
                patient.Email,
                " Appointment Confirmation ",
                $"Your appointment is confirmed with Dr. {doctor1.Doctor_Name} at {appointment.Appointment_Date}.\n" +
                $"Reason :{patient.Patient_description?? "No reason is provided"}");




            return appointment;
        }





        public async Task<bool> CancelAppointmeAsync(int id)
        {

            var appoint = await _apiconext.appointments
              .Include(a => a.patient)
              .Include(a => a.Doctor)
             .FirstOrDefaultAsync(a => a.Appoitment_Id == id);
            if (appoint == null) return false;
            if (appoint.status == "Cancelled") return false;
            appoint.status = "Cancelled";

            if (appoint.status == "Cancelled")
            {
                await _emailservices.SendEmailAsync(
                    appoint.patient.Email,
                    "  Appointment Cancelled ",
                    $"Your appointment is Cancelled with Dr. {appoint.Doctor.Doctor_Name} on {appoint.Appointment_Date:dd MMM yyyy}."
                );
            }
            _apiconext.appointments.Update(appoint);
            var result = await _apiconext.SaveChangesAsync();
            return true;
        }



        public async Task<bool> ResheduledAppointment(int id,DateTime  newdate)
        {
            var appoint = await _apiconext.appointments
              .Include(a => a.patient)
              .Include(a => a.Doctor)
             .FirstOrDefaultAsync(a => a.Appoitment_Id == id);

            if (appoint == null || appoint.status == "Cancelled")
                return false;

            appoint.Appointment_Date = newdate.Date;
            appoint.status = "Reshedule";

            if (appoint.status == "Reshedule")
            {
                await _emailservices.SendEmailAsync(
                    appoint.patient.Email,
                    " Appointment Rescheduled ",
                    $"Your appointment is rescheduled with Dr. {appoint.Doctor.Doctor_Name} on {appoint.Appointment_Date:dd MMM yyyy}."
                );
            }

            _apiconext.appointments.Update(appoint);
            var result = await _apiconext.SaveChangesAsync();
            return result > 0;
        }





        public async Task<IEnumerable<Appointment>> GetAppointmentbyDoctorandDate(int doctor_id, DateTime date)
        {
            return await _apiconext.appointments
                  .Where(a => a.Doctor_Id == doctor_id && a.Appointment_Date == date.Date && a.status == "Scheduled")
                  .Include(a => a.patient)
                  .ToListAsync();

        }
        public async Task<IEnumerable<Appointment>> GetDoctorSchedule([FromQuery] int doctor_id, [FromQuery] DateTime date)
        {
           
            return await _apiconext.appointments
               
                .Where(a => a.Doctor_Id == doctor_id && a.Appointment_Date.Day == date.Day)
                .Include(a => a.patient)
                .ToListAsync();

        }

        public async Task<List<DateTime>> GetAvailablesLotsAsync([FromQuery] int doctor_id,[FromQuery] DateTime date)
        {
            var doctor = await _apiconext.doctors.FindAsync(doctor_id);
            if (doctor == null || doctor.IsonLeave)
                return new List<DateTime>();

            // dEFINE WORKING HOUR
            DateTime starttime = date.Date.AddHours(8);
            DateTime endtime = date.Date.AddHours(17);
            DateTime breakstart = date.Date.AddHours(13);
            DateTime breakend = date.Date.AddHours(14);

            List<DateTime> slots = new();
            while (starttime < endtime)
            {
                // skip break

                if (starttime >= breakstart && starttime < breakend)
                {
                    starttime = breakend;
                    continue;
                }
                // check  if slot already book

                bool isbooked = await _apiconext.appointments
                    .AnyAsync(p => p.Doctor_Id == doctor_id && p.Appointment_Date == starttime && p.status == "Scheduled");
                if (!isbooked)
                {
                    slots.Add(starttime);
                }
                starttime = starttime.AddMinutes(30);


            }

            return slots;


        }

        private async Task LogActionAsync(string action , string performedby,string details)
        {
            var log = new Auditlog
            {

                Action = action,
                Performedby = performedby,
                PerformedAt = DateTime.Now,
                detail = details

            };
            _apiconext.Auditlogs.Add(log);

            await _apiconext.SaveChangesAsync();
            var currentuser = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "Unknown User";
            var currentRole = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == "role")?.Value ?? " Unknown Role";


        }

        
    }
}