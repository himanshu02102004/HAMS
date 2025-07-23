using Hospital_Management.Database;
using Hospital_Management.DTO;
using Hospital_Management.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hospital_Management.Services
{
    public class ReportService : IReportService
    {
        private readonly Apicontext _context;

        public ReportService(Apicontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DailyAppointmentReportDto>> GetDailyAppointmentsAsync()
        {
            var today = DateTime.Today;

            var result = await _context.appointments
                .Where(a => a.Appointment_Date.Date == today)
                //.GroupBy(a => new { a.Doctor.Name, a.Doctor.Department.Name })
                .GroupBy(a => new
                {
                    DoctorName = a.Doctor.Doctor_Name,
                    DepartmentName = a.Doctor.Department != null ? a.Doctor.Department.Department_Name
                    : "Unassigned"
                })

                .Select(g => new DailyAppointmentReportDto
                {
                    DoctorName = g.Key.DoctorName,
                    DepartmentName = g.Key.DepartmentName,
                    AppointmentCount = g.Count(),
                    Date = today
                })
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<DoctorUtilizationDto>> GetDoctorUtilizationAsync()
        {
            var totalSlots = 10; // Assume 10 slots per doctor daily for simplicity

            var today = DateTime.Today;

            var result = await _context.appointments
                .Where(a => a.Appointment_Date.Date == today)
                .GroupBy(a => a.Doctor.Doctor_Name)
                .Select(g => new DoctorUtilizationDto
                {
                    DoctorName = g.Key,
                    UtilizationPercentage = (g.Count() / (double)totalSlots) * 100
                })
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<PatientVisitFrequencyDto>> GetPatientVisitFrequenciesAsync()
        {
            var result = await _context.appointments
                .GroupBy(a => a.patient.Patient_name)
                .Select(g => new PatientVisitFrequencyDto
                {
                    PatientName = g.Key,
                    VisitCount = g.Count()
                })
                .OrderByDescending(p => p.VisitCount)
                .ToListAsync();

            return result;
        }
    }
}
