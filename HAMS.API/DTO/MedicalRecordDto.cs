namespace Hospital_Management.DTO
{
    public class MedicalRecordDto
    {

        public int Id { get; set; }
        public int Patient_id { get; set; }
        public int Doctor_Id { get; set; }
        public string Notes { get; set; }
        public DateTime? FollowUpdate { get; set; }
        public string prescribe { get; set; } = string.Empty;

        public DoctorDto doctor { get; set; }
        public PatientDto patient { get; set; }
    }
}
