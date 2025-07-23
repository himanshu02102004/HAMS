using Hospital_Management.DTO;
using Hospital_Management.Model;
using Hospital_Management.Models;
using Hospital_Management.Services;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {


        private readonly IDoctorService _doctorService;
        public DoctorController( IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var doc = await _doctorService.GetallDoctor();
            return Ok(doc);


        }
        [HttpGet("schedules")]
        public async Task<IActionResult> GetSchedules()
        {
            var schedules = await _doctorService.GetAllSchedules();

            var scheduleDtos = schedules.Select(s => new CreateDoctorScheduleDto
            {
                Doctor_Id = s.Doctor_Id,
                DayOfWeek = s.DayOfWeek,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                IsAvailable = s.IsAvailable
            }).ToList();

            return Ok(scheduleDtos);
        }





        [HttpGet("id")]
        public async Task<IActionResult> Getbyid(int id)
        {
            var docs= await _doctorService.GetDoctorbyid(id);
            return Ok(docs);
        }




        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] CreateDoctorDto dto)
        {
            var doctor = new Doctor
            {
                Doctor_Name = dto.Name,
                Doctor_Description = dto.Description,
                Doctor_specialization = dto.specialization,
                 Doctor_Availabiity= dto.availabiity,
                IsonLeave = dto.IsonLeave,
                Department_Id = dto.Department_Id
            };

            var createdDoctor = await _doctorService.AddDoctor(doctor);

            return Ok(createdDoctor);
        }






        [HttpPost("schedules")]
            public async Task <IActionResult> Add(CreateDoctorScheduleDto dto)
            {

            var sch = new DoctorSchedule
            {
                Doctor_Id = dto.Doctor_Id,
               DayOfWeek=dto.DayOfWeek,
                StartTime=dto.StartTime,
               EndTime= dto.EndTime,
               IsAvailable = true,
                CreatedAt = DateTime.Now
            };


            await _doctorService.AddDoctorSchedule(sch);
            return Ok(sch);
            }



        [HttpPut("id")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Update(int id, Doctor doctor )
        {
            if (id != doctor.Doctor_Id) return BadRequest("Not found doctor");
            var updated = await _doctorService.UpdateDoctor(doctor);
            if (!updated) return NotFound();
            return Ok(updated);


        }




        [HttpDelete("id")]

        public async  Task<IActionResult> delete(int id)
        {
            var delete= await _doctorService.DeleteDoctor(id);
            if(!delete) return NotFound();
            return Ok("delete successfuly");

        }

        [HttpPatch("{id}/isonleave")]

        public async Task<IActionResult> Partialupdate(int id, bool IsonLeave)
        {

            var partial= await _doctorService.Markonleave(id, IsonLeave);
            if (!partial) return NotFound();
            return Ok("partial update successfully");
           
        }

        [HttpPatch("{id}/leave")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MarkOnLeave(int id, [FromQuery] bool isOnLeave)
        {
            var result = await _doctorService.Markonleave(id, isOnLeave);
            if (!result) return NotFound();
            return NoContent();
        }




    }
}
