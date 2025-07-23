namespace Hospital_Management.DTO
{
    public class CreateDoctorScheduleDto
    {

        public int Doctor_Id { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}
