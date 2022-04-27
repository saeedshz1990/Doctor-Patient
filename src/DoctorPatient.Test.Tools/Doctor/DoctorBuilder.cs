using System;
using DoctorPatient.Entities;

namespace DoctorPatient.Test.Tools.Doctor
{
    public class DoctorBuilder
    {
        private Entities.Doctor doctor;

        public DoctorBuilder()
        {
            doctor = new Entities.Doctor
            {
                FirstName = "Hossein",
                LastName = "Khani",
                NationalCode = "123456",
                Field = ""
            };
        }

        public DoctorBuilder WithAppointment(DateTime date, string firstName,
            string lastName, string patientNationlCode)
        {
            doctor.Appointments.Add(new Entities.Appointment
            {
                Date = date,
                Patient = new Patient
                {
                    FirstName = firstName,
                    LastName = lastName,
                    NationalCode = patientNationlCode,
                }
            });

            return this;
        }

        public Entities.Doctor Build()
        {
            return doctor;
        }
    }
}
