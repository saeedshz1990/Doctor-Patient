using System.Linq;
using DoctorPatient.Entities;
using DoctorPatient.Services.Doctors.Contracts;

namespace DoctorPatient.Persistence.EF.Doctors
{
    public class EFDoctorRepository : DoctorRepository
    {
        private readonly EFDataContext _context;

        public EFDoctorRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
        }

        public bool IsNationalCodeExist(string nationalCode)
        {
           return _context
               .Doctors
               .Any(d => d.NationalCode == nationalCode);
        }
    }
}
