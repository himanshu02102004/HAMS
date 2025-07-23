using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model
{
    public class Patient
    {
        [Key]
        public int Patient_id { get; set; }
        public string Patient_name { get; set; }
        public string patient_phoneNo { get; set; }
        public string  Email { get; set; }

        public string Patient_description { get; set; } = string.Empty;

    }
}