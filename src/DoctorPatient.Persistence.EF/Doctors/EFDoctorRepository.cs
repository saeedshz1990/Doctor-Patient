using System.Collections.Generic;
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

        public IList<GetDoctorDto> GetAll()
        {
            var doctor = _context
                .Doctors
                .Select(_ => new GetDoctorDto
                {
                    Id = _.Id,
                    FirstName = _.FirstName,
                    LastName = _.LastName,
                    Field = _.Field,
                    NationalCode = _.NationalCode,
                }).ToList();
            return doctor;
        }

        public void Update(Doctor doctor, int id)
        {

        }

        public Doctor FindById(int id)
        {
            return _context.Doctors.Find(id);
        }

        public bool IsExistNationalCode(string nationalCode)
        {
            return _context.Doctors.Any(_ => _.NationalCode == nationalCode);
        }

        public void Delete(int id)
        {
            var doctor = _context
                .Doctors
                .FirstOrDefault(_ => _.Id == id);

            _context.Remove(doctor);
        }
    }
}
