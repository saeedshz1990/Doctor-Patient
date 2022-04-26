using System.Collections.Generic;
using DoctorPatient.Infrastructure.Application;

namespace DoctorPatient.Services.Doctors.Contracts
{
    public interface DoctorService :Service
    {
        void Add(AddDoctorDto dto);
        IList<GetDoctorDto> GetAll();
        void Update(UpdateDoctorDto dto ,int id);
        void Delete(int id);
    }
}
