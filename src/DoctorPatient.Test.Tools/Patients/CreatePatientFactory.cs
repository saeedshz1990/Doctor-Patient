using DoctorPatient.Entities;

namespace DoctorPatient.Test.Tools.Patients
{
    public static class CreatePatientFactory
    {
        public static Patient Create(string firstName, string lastName,
            string nationalCode)
        {
            return new Patient
            {
                FirstName = firstName,
                LastName = lastName,
                NationalCode = nationalCode,
            };
        }
    }
}
