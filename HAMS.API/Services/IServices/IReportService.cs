using Hospital_Management.DTO;

namespace Hospital_Management.Services.IServices
{
    public interface IReportService
    {

        Task<IEnumerable<DailyAppointmentReportDto>> GetDailyAppointmentsAsync();
        Task<IEnumerable<DoctorUtilizationDto>> GetDoctorUtilizationAsync();
        Task<IEnumerable<PatientVisitFrequencyDto>> GetPatientVisitFrequenciesAsync();
    }
}
