using Hospital_Management.Database;
using Hospital_Management.DTO;
using Hospital_Management.Model;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospital_Management.Controllers
{

    [ApiController]
    [Route("api/controller")]
    //[Authorize]

    public class AppointmentController: ControllerBase
    {
        private readonly IAppointmentServices _appointmentServices;
        private readonly Apicontext _apicontext;

        public AppointmentController( IAppointmentServices appointmentServices, Apicontext apicontext)
        {
             _appointmentServices = appointmentServices;
            _apicontext = apicontext;
        }


        [HttpPost]
      //  [Authorize(Roles = "Receptionist,Admin")]

        public async Task<IActionResult> bookappointment(BookAppointmentDto dto)
        {
            if (dto == null) return BadRequest(" Invalid appointment data");
            var appoint = new Appointment
            {
                Doctor_Id=dto.doctor_ID,
              Patient_id =dto.Patient_ID,
               Appointment_Date=dto.dateTime,
             
              status="Scheduled"
            };
            var bookappoint= await _appointmentServices.BookAppointment(appoint);
            if (bookappoint == null) return BadRequest("SLOT NOT AVAILABLE OR DOCTOR UNAVAILABLE");
            return Ok(bookappoint);
        }



        [HttpPatch("cancel")]
      //  [Authorize(Roles = "Receptionist,Admin")]
        public async Task<IActionResult> cancelappointment(int id)
        {
            var result= await _appointmentServices.CancelAppointmeAsync(id);
            if (!result) return BadRequest("not available appointment or Appointment get cancel ");
            return Ok("successfully cancel appointment");
        }

        [HttpPatch("resheduled")]
    //    [Authorize(Roles = "Receptionist,Admin")]
        public async Task<IActionResult> resheduled(int id, DateTime date)
        {
           

            var appoint= await _appointmentServices.ResheduledAppointment(id, date);
            if (!appoint) return NotFound("apointment is not found");
            return Ok("Appointment resheduled");


        }

        [HttpGet("doctor-schedule")]
        [Authorize(Roles = "Receptionist,Admin,Doctor")]

        public async Task<IActionResult> getdoctorschedule( int doc_id, DateTime date)
        {

            var Doccexist = await _apicontext.doctors.AnyAsync(a => a.Doctor_Id == doc_id);
            if (!Doccexist) return  NotFound("doctor is not found");
            var schedule= await _appointmentServices.GetDoctorSchedule(doc_id,  date);
            return Ok(schedule);
        }


        [HttpGet("available-slot")]

        public async Task<IActionResult> GetAvailableslot(int doctorid, DateTime date)
        {
            var slots= await _appointmentServices.GetAvailablesLotsAsync(doctorid,date);
            return Ok(slots);
        }




    }
}
