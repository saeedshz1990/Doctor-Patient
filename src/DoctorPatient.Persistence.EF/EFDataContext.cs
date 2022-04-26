using System;
using DoctorPatient.Entities;
using DoctorPatient.Persistence.EF.Doctors;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatient.Persistence.EF
{
    public class EFDataContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
        {
        }
        //public EFDataContext(string connectionString) :
        //    this(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        //{ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorEntityMap).Assembly);
        }
    }
}
