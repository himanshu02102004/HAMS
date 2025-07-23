using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model
{
    public class Department
    {
        [Key]
        public int Department_Id { get; set; }
        
        public string Department_Name { get; set; }
        public string Department_Description { get; set; }



       
       

    }
}
