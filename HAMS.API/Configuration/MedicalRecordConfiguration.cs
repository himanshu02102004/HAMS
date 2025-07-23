
using Hospital_Management.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital_Management.Configuration
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {

        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasKey(m => m.ID);

            builder.HasOne(m => m.doctor)
                .WithMany()
                .HasForeignKey(m => m.Doctor_Id)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade

            builder.HasOne(m => m.Patient)
                .WithMany()
                .HasForeignKey(m => m.Patient_id)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade

            builder.Property(m => m.prescribe)
                .IsRequired()
                .HasDefaultValue(string.Empty);
        }
    }
}
