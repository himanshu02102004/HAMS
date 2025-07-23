using Microsoft.AspNetCore.Components.Web;

namespace Hospital_Management.DTO
{
    public class CreateDoctorDto
    {


        public string Name { get; set; }
      
        public string Description { get; set; }
        public string specialization { get; set; }
        public string availabiity { get; set; }
        public bool IsonLeave { get; set; }
        public int Department_Id { get; set; }

       
    }
}
