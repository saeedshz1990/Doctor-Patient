namespace DoctorPatient.Test.Tools.Doctor
{
    public static class CreateDoctorFactory
    {
        public static Entities.Doctor Create(string firstName, string lastName,
            string nationalCode, string field)
        {
            return new Entities.Doctor
            {
                FirstName = firstName,
                LastName = lastName,
                NationalCode = nationalCode,
                Field = field
            };
        }
    }
}
