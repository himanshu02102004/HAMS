using Hospital_Management.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management.Models
{
    public class DoctorSchedule
    {
        [Key]
        public int Schedule_Id { get; set; }

     
        public int Doctor_Id { get; set; }

       
        public string DayOfWeek { get; set; }  // e.g., "Monday"

    
        public TimeSpan StartTime { get; set; }

    
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("Doctor_Id")]
        public Doctor Doctor { get; set; }
    }
}
