using System;
using System.Collections.Generic;
using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;

namespace DoctorPatient.Services.Appointments.Contracts
{
    public interface AppointmentRepository :Repository
    {
        void Add(Appointment appointment);
        int GetCountOfDoctorsAppointments(int doctorId,int patientId ,DateTime date);
        List<GetAppointmentDto> GetAll();
        void Update(int id, Appointment appointment);
        Appointment FindById(int id);
        int GetAppointmentCountByDoctorId(int doctorId);
        void Delete(int id);
    }
}
