using System;
using DoctorPatient.Entities;

namespace DoctorPatient.Test.Tools.Appointment
{
    public static class CreateAppoinmentFactory
    {
        public static Entities.Appointment Create(Entities.Doctor doctor, Patient patient)
        {
            return new Entities.Appointment
            {
               Date = DateTime.Today,
               Doctor = doctor,
               Patient = patient
            };
        }
    }
}
