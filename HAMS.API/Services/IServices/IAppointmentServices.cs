
using Hospital_Management.Model;


namespace Hospital_Management.Services.IServices
{
    public interface IAppointmentServices
    {

      public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        public Task<Appointment> GetAppointmentbyID(int id);
        public Task<Appointment> BookAppointment(Appointment appointment);
        public Task<bool> CancelAppointmeAsync(int id);
        public Task<bool> ResheduledAppointment(int id, DateTime newdate);
        
        // Doctor role 
        public Task<IEnumerable<Appointment>> GetAppointmentbyDoctorandDate(int doctor_id,  DateTime date);

        public Task<IEnumerable<Appointment>> GetDoctorSchedule(int doctor_id, DateTime date);
        public Task<List<DateTime>> GetAvailablesLotsAsync(int Doctor_id,DateTime date);






    }
}
