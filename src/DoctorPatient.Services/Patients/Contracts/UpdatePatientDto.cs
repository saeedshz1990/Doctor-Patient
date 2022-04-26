namespace DoctorPatient.Services.Patients.Contracts
{
    public class UpdatePatientDto
    {
        public int Id { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
