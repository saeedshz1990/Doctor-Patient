using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;
using DoctorPatient.Services.Patients.Contracts;
using DoctorPatient.Services.Patients.Exceptions;

namespace DoctorPatient.Services.Patients
{
    public class PatientAppService : PatientService
    {
        private readonly PatientRepository _patientRepository;
        private readonly UnitOfWork _unitOfWork;

        public PatientAppService(UnitOfWork unitOfWork, PatientRepository patientRepository)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
        }

        public void Add(AddPatientDto dto)
        {
            var nationalCode = _patientRepository
                .IsNationalCodeExist(dto.NationalCode);
            var doctor = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                NationalCode = dto.NationalCode
            };

            if (nationalCode == false)
            {

                _patientRepository.Add(doctor);
                _unitOfWork.Commit();
            }
            else
            {
                throw new PatientNationalCodeExistException();
            }
        }
    }
}
