using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model
{
    public class MedicalRecord
    {


        [Key]
        public int ID { get; set; }
        public int Patient_id { get; set; }

        public int Doctor_Id { get; set; }
        public string Notes { get; set; }

        public DateTime ? FollowUpdate { get; set; }
        public string   prescribe { get; set; }= string.Empty;
        public Doctor doctor { get; set; }
        public Patient Patient { get; set; }

    }
}
