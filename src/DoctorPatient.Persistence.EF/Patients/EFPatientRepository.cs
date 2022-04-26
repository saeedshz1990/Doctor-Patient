using System.Collections.Generic;
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

        public IList<GetPatientDto> GetAll()
        {
            var patient = _context
                .Patients
                .Select(_ => new GetPatientDto
                {
                    Id = _.Id,
                    FirstName = _.FirstName,
                    LastName = _.LastName,
                    NationalCode = _.NationalCode,
                }).ToList();
            return patient;
        }

        public void Update(Patient patient, int id)
        {
            
        }

        public Patient FindById(int id)
        {
            return _context.Patients.Find(id);
        }

        public bool IsExistNationalCode(string nationalCode)
        {
            return _context
                .Patients
                .Any(d => d.NationalCode == nationalCode);
        }

        public void Delete(int id)
        {
            var patient = _context
                .Patients
                .FirstOrDefault(_ => _.Id == id);

            _context.Remove(patient);
        }
    }
}
