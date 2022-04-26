using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;

namespace DoctorPatient.Services.Doctors.Contracts
{
    public interface DoctorRepository :Repository
    {
        void Add(Doctor doctor);
        bool IsNationalCodeExist(string nationalCode);
    }
}
