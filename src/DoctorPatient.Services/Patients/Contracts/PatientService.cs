using System.Collections.Generic;
using DoctorPatient.Infrastructure.Application;

namespace DoctorPatient.Services.Patients.Contracts
{
    public interface PatientService : Service
    {
        void Add(AddPatientDto dto);
        IList<GetPatientDto> GetAll();
        void Update(UpdatePatientDto dto, int id);
    }
}
