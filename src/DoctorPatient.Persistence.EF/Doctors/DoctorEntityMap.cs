using DoctorPatient.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorPatient.Persistence.EF.Doctors
{
    public class DoctorEntityMap :IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> _)
        {
            _.ToTable("Doctors");

            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            _.Property(_ => _.NationalCode)
                .IsRequired()
                .HasMaxLength(10);

            _.Property(_ => _.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            _.Property(_ => _.LastName)
                .IsRequired()
                .HasMaxLength(50);

            _.Property(_ => _.Field)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
