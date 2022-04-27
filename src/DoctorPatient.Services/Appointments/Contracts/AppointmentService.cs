using System.Collections.Generic;
using DoctorPatient.Infrastructure.Application;

namespace DoctorPatient.Services.Appointments.Contracts
{
    public interface AppointmentService :Service
    {
        void Add(AddAppointmentDto dto);
        List<GetAppointmentDto> GetAll();
        void Update(int id, UpdateAppointmentDto dto);
        void Delete(int id);
    }
}
