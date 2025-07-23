using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model
{
    public class Appointment
    {
        [Key]
        public int Appoitment_Id { get; set; }
        public int Patient_id { get; set; }
       

        public int Doctor_Id { get; set; }



        public DateTime Appointment_Date { get; set; }
        public string  status { get; set; }

        public Patient patient { get; set; }

        public Doctor Doctor { get; set; }

    }
}
