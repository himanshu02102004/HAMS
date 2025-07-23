using Hospital_Management.Database;
using Hospital_Management.DTO;
using Hospital_Management.Model;
using Hospital_Management.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Hospital_Management.Services
{
    public class DepartmentServices : IDepartmentServices
    {

        private readonly Apicontext _apicontext;
        public DepartmentServices( Apicontext  apicontext)
        {
         _apicontext = apicontext;   
        }

        public async Task<IEnumerable<Department>> GetAlldepartment()
        {
           var exist= await _apicontext.departments.ToListAsync();
            if(exist == null)
            {
                return null;
            }   
            return exist;
        }




        public async Task<Department> GetDepartmentbyID(int id) =>await  _apicontext.departments.FindAsync(id);
           
      


        public async Task<Department> CreateDepartment( CreateDepartment create)
        {
            var dep = new Department
            {
               // Department_Id = create.Department_id,
                Department_Name=create.Department_name,
                Department_Description=create.Department_description

            };

            _apicontext.Add(dep);
            await _apicontext.SaveChangesAsync();
            return dep;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            var found = await _apicontext.departments.FindAsync(id);
            if(found == null)
            {
                return false;
            }
            _apicontext.departments.Remove(found);
            await _apicontext.SaveChangesAsync();
            return true;

        }

       
        public async Task<bool> UpdateDepartment(int id, UpdateDepartmentDTO department )
        {
            var exist = await _apicontext.departments.FindAsync(id);
                if(exist == null)   
                { return false; }
            exist.Department_Name = department.Department_Name;
            exist.Department_Description=department.Department_Description;
            await _apicontext.SaveChangesAsync();
            return true;

        }


        public async Task<bool> Markonleave(int id, bool IsonLeave)
        {
            var ex= await _apicontext.doctors.FindAsync(id);
            if(ex == null)
                { return false; }
            ex.IsonLeave= IsonLeave;
            await _apicontext.SaveChangesAsync();
                return true;
        }

      
    }
}
