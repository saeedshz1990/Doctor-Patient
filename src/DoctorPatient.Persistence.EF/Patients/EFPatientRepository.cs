using System.Linq;
using DoctorPatient.Entities;
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

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
        }

        public bool IsNationalCodeExist(string nationalCode)
        {
            return _context
                .Patients
                .Any(p => p.NationalCode == nationalCode);
        }
    }
}
