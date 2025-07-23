
namespace Hospital_Management.DTO
{
    public class DailyAppointmentReportDto
    {

        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public int AppointmentCount { get; set; }
        public DateTime Date { get; set; }
    }
}
