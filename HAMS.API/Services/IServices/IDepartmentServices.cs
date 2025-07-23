using Hospital_Management.DTO;
using Hospital_Management.Model;

namespace Hospital_Management.Services.IServices
{
    public interface  IDepartmentServices
    {

        public Task<IEnumerable<Department>> GetAlldepartment();

        public Task<Department> GetDepartmentbyID(int id);
        public Task<Department> CreateDepartment(CreateDepartment department);
        public Task<bool>  UpdateDepartment(int id, UpdateDepartmentDTO department);
        public Task<bool> DeleteDepartment(int id);



    }
}
