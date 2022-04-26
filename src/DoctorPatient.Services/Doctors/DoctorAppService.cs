using System.Collections.Generic;
using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;
using DoctorPatient.Services.Doctors.Contracts;
using DoctorPatient.Services.Doctors.Exceptions;

namespace DoctorPatient.Services.Doctors
{
    public class DoctorAppService : DoctorService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly DoctorRepository _doctorRepository;
        public DoctorAppService(UnitOfWork unitOfWork, DoctorRepository doctorRepository)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
        }

        public void Add(AddDoctorDto dto)
        {
            var nationalCode = _doctorRepository
                .IsNationalCodeExist(dto.NationalCode);
            var doctor = new Doctor
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Field = dto.Field,
                NationalCode = dto.NationalCode
            };
            
            if (nationalCode==false)
            {
               
                _doctorRepository.Add(doctor);
                _unitOfWork.Commit();
            }
            else
            {
                throw new DoctorNationalCodeExistException();
            }

        }

        public IList<GetDoctorDto> GetAll()
        {
            return _doctorRepository.GetAll();
        }
    }
}
