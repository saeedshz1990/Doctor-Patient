using System;

namespace DoctorPatient.Services.Appointments.Contracts
{
    public class GetAppointmentDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
