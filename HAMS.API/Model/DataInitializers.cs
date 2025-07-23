using Azure.Identity;
using Hospital_Management.Database;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;

namespace Hospital_Management.Model
{
    public class DataInitializers
    {

        public static void SetAdmin(Apicontext apicontext)
        {
            if(!apicontext.Users.Any(u => u.Role == "Admin"))
            {
                var admin = new UserApplication
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),  // use a strong default password
                    Role = "Admin"
                }; 

                apicontext.Users.Add(admin);
                apicontext.SaveChanges();


            }


        }
    }
}
