using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;

namespace DoctorPatient.Services.Patients.Contracts
{
    public interface PatientService : Service
    {
        void Add(AddPatientDto dto);
    }
}
