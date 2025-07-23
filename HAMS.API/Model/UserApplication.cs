namespace Hospital_Management.Model
{
    public class UserApplication
    {


        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // "Admin", "Doctor", "Reception"
    }
}
