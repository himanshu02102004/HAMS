using Hospital_Management.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hospital_Management.Model
{
    public class Doctor
    {
        [Key]
        public int Doctor_Id { get; set; }
       public string Doctor_Name { get; set; }
        public string Doctor_Description { get; set; }
        public string Doctor_specialization { get; set; }
      
        public string Doctor_Availabiity { get; set; }


        public bool IsonLeave { get; set; }



        [ForeignKey("Department")]
        public int Department_Id { get; set; }
        public Department Department { get; set; }
        [JsonIgnore]
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; } // Add this

    }
}
