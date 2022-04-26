using FluentMigrator;

namespace DoctorPatient.Migrations
{
    [Migration(202204261227)]
    public class _202204261227_InitialDatabase : Migration
    {
        public override void Up()
        {
            CreateDoctor();
            CreatePatient();
            CreateAppointment();
        }

        public override void Down()
        {
            Delete.Table("Appointments");
            Delete.Table("Patients");
            Delete.Table("Doctors");
        }

        private void CreateAppointment()
        {
            Create.Table("Appointments")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Date").AsDate()
                .WithColumn("DoctorId").AsInt32().NotNullable()
                .ForeignKey("FK_Doctors_Appointments", "Doctors", "Id")
                .WithColumn("PatientId").AsInt32().NotNullable()
                .ForeignKey("FK_Patients_Appointments", "Patients", "Id");
        }

        private void CreatePatient()
        {
            Create.Table("Patients")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("NationalCode").AsString(10).NotNullable();
        }

        private void CreateDoctor()
        {
            Create.Table("Doctors")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("NationalCode").AsString(10).NotNullable()
                .WithColumn("Field").AsString(50).NotNullable();
        }
    }
}
