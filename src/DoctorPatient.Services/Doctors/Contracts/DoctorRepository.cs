using System;
using System.Collections.Generic;
using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;

namespace DoctorPatient.Services.Doctors.Contracts
{
    public interface DoctorRepository :Repository
    {
        void Add(Doctor doctor);
        bool IsNationalCodeExist(string nationalCode);
        IList<GetDoctorDto> GetAll();
        void Update(Doctor doctor ,int id);
        Doctor FindById(int id);
        bool IsExistNationalCode(string nationalCode);
        void Delete(int id);
    }
}
