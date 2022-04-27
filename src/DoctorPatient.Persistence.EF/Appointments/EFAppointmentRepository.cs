using System;
using System.Collections.Generic;
using System.Linq;
using DoctorPatient.Entities;
using DoctorPatient.Services.Appointments.Contracts;

namespace DoctorPatient.Persistence.EF.Appointments
{
    public class EFAppointmentRepository : AppointmentRepository
    {
        private readonly EFDataContext _context;

        public EFAppointmentRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
        }

        public int GetCountOfDoctorsAppointments(int doctorId, int patientId, DateTime date)
        {
            var getCountAppoinment = _context
                .Appointments.Count(x => x.DoctorId == doctorId
                                         && x.PatientId == patientId &&
                                         x.Date == date);
            return getCountAppoinment;
        }

        public List<GetAppointmentDto> GetAll()
        {
            return _context
                .Appointments
                .Select(_ => new GetAppointmentDto
                {
                    Id = _.Id,
                    Date = _.Date,
                    PatientId = _.PatientId,
                    DoctorId = _.DoctorId
                }).ToList();
        }

        public void Update(int id, Appointment appointment)
        {

        }

        public Appointment FindById(int id)
        {
            return _context.Appointments.Find(id);
        }

        public int GetAppointmentCountByDoctorId(int doctorId)
        {
            return _context.Appointments.Count(_ => _.DoctorId == doctorId);
        }
    }
}
