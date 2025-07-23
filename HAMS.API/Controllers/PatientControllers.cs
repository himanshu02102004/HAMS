
using Hospital_Management.Database;
using Hospital_Management.DTO;
using Hospital_Management.Model;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Hospital_Management.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PatientControllers: ControllerBase
    {
        private readonly Apicontext _context;

        private readonly IPatientServices _patientServices;

        public PatientControllers( IPatientServices patientServices, Apicontext apicontext)
        {
         _patientServices = patientServices;
            _context = apicontext;
        }







        [HttpGet]

        public async Task<IActionResult> Patientall()
        {
            var info = await _patientServices.GetAllPatient();
            if (info == null) return BadRequest("none of patient ");
            return Ok(info);
        }


        [HttpGet("id")]

        public async Task<IActionResult> Patientbyid(int id)
        {
            var infos= await _patientServices.GetPatientById(id);
            if (infos == null) return NotFound();
            return Ok(infos);
        }


        [HttpPost("createpatient")]

        public async Task<IActionResult> AddPatient(CreatePatientDto dto)
        {
            var cus = new Patient
            { 
               Patient_name= dto.Patient_name,
               patient_phoneNo=dto.Patient_Phone,
               Patient_description=dto.Patient_description,
                   Email=dto.Patient_Email

            };

            await _patientServices.AddPatient(cus); 
            return Ok(cus);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Patientdelete(int id)
        {
            var delete = await _patientServices.DeletePatient(id);
            if (delete == null) return NotFound();
            
            return Ok(new { message ="patient delete succesfully"});

        }

        [HttpPut("Updatebyid")]
        [Authorize(Roles = "Receptionist,Admin")]
        public async Task<IActionResult> PatientUpdate(int id,[FromBody] PatientUpdateDto patientUpdateDto)
        {
            var updatepatiented = await _patientServices.UpdatePatient(id, patientUpdateDto);
            if(updatepatiented == null)
            {
                return NotFound(new { message = " patient is not found" });
            }
            return Ok(new
            {
                message = "patient updated succesfuly",
                data=updatepatiented /// 

            }); 


           
        }


        [HttpPatch("Partialupdate/{id}")]
        [Authorize(Roles = "Receptionist,Admin")]
        public async Task<IActionResult> partialupdate(int id, [FromBody] PatientPartialUpdateDto patientPartialUpdateDto)
        {
            var result = await _patientServices.PartialUpdate(id, patientPartialUpdateDto);
            if (!result)
                return Ok(new { message = "patient is not found" });
            return Ok(new { message = "patient partially updated succesffully" });



        }

        [HttpGet("Search")]
        [Authorize(Roles = "Receptionist,Admin")]
        public async Task<IActionResult> SearchPatient(string query)
        {


            var patient= await _patientServices.SearchPatient(query);
         if (patient == null || !patient.Any()) return BadRequest("No matching patient found");

         return Ok(patient);


        }





        [HttpGet("test-exception")]
        public IActionResult ThrowError()
        {
            throw new Exception("this is test exception");
        }


    }
}
