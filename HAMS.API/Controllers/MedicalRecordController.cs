using Hospital_Management.DTO;
using Hospital_Management.Model;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{

    [ApiController]
    [Route("api/medical-record")]
    public class MedicalRecordController : ControllerBase
    {


        private readonly IMedicalRecordServices medicalrecordservices;
        public MedicalRecordController( IMedicalRecordServices _medicalrecordservices)
        {

            medicalrecordservices = _medicalrecordservices;
        }

        [HttpGet]      
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        public async Task<IActionResult> GetAll()
        {
            var record = await medicalrecordservices.GetPrescriptionListAsync();
            return Ok(record);


        }


        [HttpGet("prescription-id")]      
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        public async Task<IActionResult> GetbyPrescriptionid(int id)
        {
            var record= await medicalrecordservices.GetPrescriptionByIdAsync(id);
            if (record == null) {
                return BadRequest("Prescription is not available");
            }

            return Ok(record);
        }

        [HttpGet("patient-id")]
        [Authorize(Roles = "Admin,Receptionist,Doctor")]
        public async Task<IActionResult> GetbyPatientid( int patientid)
        {

            var pat = await medicalrecordservices.GetbyPatientid(patientid);
            return Ok(pat);


        }
        [HttpPost("add")]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Add( MedicalRecordDto record)
        {

          


          var aded= await medicalrecordservices.AddAsync(record);
            return CreatedAtAction(nameof(GetbyPrescriptionid), new { id = aded.ID}, aded);
          
        }
    }
}