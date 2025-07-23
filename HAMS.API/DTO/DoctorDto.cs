namespace Hospital_Management.DTO
{
    public class DoctorDto
    {

        public string doctor_Name { get; set; }
        public string doctor_Description { get; set; }
        public string doctor_specialization { get; set; }
        public string doctor_Availabiity { get; set; }
        public bool isonLeave { get; set; }
        public int department_Id { get; set; }
    }
}
