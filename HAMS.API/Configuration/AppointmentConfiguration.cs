using Hospital_Management.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital_Management.Configuration
{
    public class AppointmentConfiguration: IEntityTypeConfiguration<Appointment>
    {

        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Appoitment_Id);

            builder.HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.Doctor_Id)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade

            builder.HasOne(a => a.patient)
                .WithMany()
                .HasForeignKey(a => a.Patient_id)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade

            builder.Property(a => a.status)
                .IsRequired();
        }


    }
}
