using System.Collections.Generic;
using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;

namespace DoctorPatient.Services.Patients.Contracts
{
    public interface PatientRepository :Repository
    {
        void Add(Patient patient);
        bool IsNationalCodeExist(string nationalCode);
        IList<GetPatientDto> GetAll();
        void Update(Patient patient, int id);
        Patient FindById(int id);
        bool IsExistNationalCode(string nationalCode);
        void Delete(int id);
    }
}
