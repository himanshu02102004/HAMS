using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Model
{
    public class Auditlog
    {
        [Key]
        public int  Id { get; set; }
        public string Action {  get; set; }
        public string Performedby { get; set; }
        public DateTime PerformedAt { get; set; }
        public string detail { get; set; }

    }
}
