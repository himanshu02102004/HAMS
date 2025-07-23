using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    [ApiController]
    [Route("api/Dashboard")]
    [Authorize(Roles ="Admin,Receptionist")]
    public class DashboardController:ControllerBase
    {

        private readonly IReportService _reportService;

        public DashboardController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("daily-appointments")]
        public async Task<IActionResult> GetDailyAppointments()
        {
            var result = await _reportService.GetDailyAppointmentsAsync();
            return Ok(result);
        }

        [HttpGet("doctor-utilization")]
        public async Task<IActionResult> GetDoctorUtilization()
        {
            var result = await _reportService.GetDoctorUtilizationAsync();
            return Ok(result);
        }

        [HttpGet("patient-visit-frequency")]
        public async Task<IActionResult> GetPatientVisitFrequency()
        {
            var result = await _reportService.GetPatientVisitFrequenciesAsync();
            return Ok(result);
        }
    }
}

