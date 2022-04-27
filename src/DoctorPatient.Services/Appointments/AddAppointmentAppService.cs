using System;
using System.Collections.Generic;
using System.Linq;
using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;
using DoctorPatient.Services.Appointments.Contracts;
using DoctorPatient.Services.Appointments.Exceptions;

namespace DoctorPatient.Services.Appointments
{
    public class AddAppointmentAppService : AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly UnitOfWork _unitOfWork;

        public AddAppointmentAppService(UnitOfWork unitOfWork, AppointmentRepository appointmentRepository)
        {
            _unitOfWork = unitOfWork;
            _appointmentRepository = appointmentRepository;
        }

        public void Add(AddAppointmentDto dto)
        {
            var countPatient = _appointmentRepository
                 .GetCountOfDoctorsAppointments(dto.PatientId, dto.PatientId, dto.Date);

            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                Date = dto.Date,
            };

            if (dto.Date == DateTime.Now.Date && appointment.PatientId > 2)
            {
                throw new AppointmentDateException();
            }

            if (countPatient >= 5)
            {
                throw new DoctorOrPatientCannotPermittedException();
            }
            else
            {
                _appointmentRepository.Add(appointment);
                _unitOfWork.Commit();
            }
        }

        public List<GetAppointmentDto> GetAll()
        {
            return _appointmentRepository.GetAll();
        }

        public void Update(int id, UpdateAppointmentDto dto)
        {
            var appionment = _appointmentRepository.FindById(id);

            if (appionment == null)
            {
                throw new DoctorDoesNotChangeException();
            }

            var doctorAppointment = _appointmentRepository
                .GetAppointmentCountByDoctorId(appionment.DoctorId);
            if (doctorAppointment >= 5)
            {
                throw new DoctorCannotgreaterthanpermittedGetAppointmentException();
            }

            dto.Date = appionment.Date;
            dto.DoctorId = appionment.DoctorId;


            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var appointment = _appointmentRepository.FindById(id);
            if (appointment != null)
            {
                _appointmentRepository.Delete(id);
                _unitOfWork.Commit();
            }
            else
            {
                throw new AppointmentNotFoundException();
            }
        }
    }
}
