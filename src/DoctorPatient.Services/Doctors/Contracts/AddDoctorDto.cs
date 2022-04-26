namespace DoctorPatient.Services.Doctors.Contracts
{
    public class AddDoctorDto
    {
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Field { get; set; }
    }
}
