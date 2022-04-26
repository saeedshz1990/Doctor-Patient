using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;

namespace DoctorPatient.Services.Patients.Contracts
{
    public interface PatientRepository :Repository
    {
        void Add(Patient patient);
        bool IsNationalCodeExist(string nationalCode);
    }
}
