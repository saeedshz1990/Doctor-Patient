using DoctorPatient.Services.Patients.Contracts;

namespace DoctorPatient.Persistence.EF.Patients
{
    public class EFPatientRepository : PatientRepository
    {
        private readonly EFDataContext _context;

        public EFPatientRepository(EFDataContext context)
        {
            _context = context;
        }
    }
}
