using System;
using System.Collections.Generic;
using System.Linq;
using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;
using DoctorPatient.Infrastructure.Test;
using DoctorPatient.Persistence.EF;
using DoctorPatient.Persistence.EF.Appointments;
using DoctorPatient.Services.Appointments;
using DoctorPatient.Services.Appointments.Contracts;
using DoctorPatient.Services.Appointments.Exceptions;
using DoctorPatient.Test.Tools.Appointment;
using DoctorPatient.Test.Tools.Doctor;
using DoctorPatient.Test.Tools.Patients;
using FluentAssertions;
using Xunit;

namespace DoctorPatient.Services.Test.Unit.Appointments
{
    public class AppointmentServiceTest
    {
        private readonly EFDataContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly AppointmentRepository _appointmentRepository;
        private readonly AppointmentService _sut;

        public AppointmentServiceTest()
        {
            _context = new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _appointmentRepository = new EFAppointmentRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new AddAppointmentAppService(_unitOfWork, _appointmentRepository);
        }

        [Fact]
        public void Add_adds_Appointment_Properly()
        {
            Doctor doctor = CreateDoctorFactory.Create("Saeed", "Ansari",
                "2280509504", "Brain");
            _context.Manipulate(_ => _.Doctors.Add(doctor));

            Patient patient = CreatePatientFactory.Create("Saeed", "Ansari",
                "2280509504");
            _context.Manipulate(_ => _.Patients.Add(patient));

            AddAppointmentDto dto = GenerateAddAppointmentDto(doctor.Id, patient.Id);

            _sut.Add(dto);

            var expected = _context.Appointments.FirstOrDefault();
            expected.Date.Should().Be(dto.Date);
            expected.DoctorId.Should().Be(dto.DoctorId);
            expected.PatientId.Should().Be(dto.PatientId);
        }

        [Fact]
        public void AddThrow_Exception_WhenPatientdoesnot_Morethanof_Two()
        {
            Doctor doctor = CreateDoctorWithFiveAppointmentInOneDay();
            _context.Manipulate(_ => _.AddRange(doctor));
            var patient = CreatePatientFactory.Create("saeed", "ansari", "2280509504");
            _context.Manipulate(_ => _.Add(patient));

            var dto = new AddAppointmentDto
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                Date = DateTime.Now.Date
            };

            Action expected = () => _sut.Add(dto);

            expected.Should().ThrowExactly<AppointmentDateException>();
        }
        private static Doctor CreateDoctorWithFiveAppointmentInOneDay()
        {
            return new DoctorBuilder()
                .WithAppointment(DateTime.Now.Date, "as", "fr", "7878")
                .WithAppointment(DateTime.Now.Date, "gt", "nb", "6565")
                .WithAppointment(DateTime.Now.Date, "ld", "yv", "4030")
                .WithAppointment(DateTime.Now.Date, "mk", "qw", "6598")
                .WithAppointment(DateTime.Now.Date, "nt", "cw", "1245")
                .Build();
        }
        [Fact]
        public void GetAll_Appointments_return_patientAndDoctor()
        {
            var doctor = CreateDoctorFactory.Create("Saeed", "Ansari",
                "2280509504", "Brain");
            _context.Manipulate(_ => _.Doctors.AddRange(doctor));
            var patient = CreatePatientFactory.Create("Saeed", "Ansari",
                "2280509504");
            _context.Manipulate(_ => _.Patients.AddRange(patient));
            var appointments = GenerateAddAppointmentDto(patient.Id, doctor.Id);
            _sut.Add(appointments);
            var expected = _sut.GetAll();

            expected.Should().HaveCount(1);
        }

        [Fact]
        public void Update_updates_appoint()
        {
            var firstDoctor = CreateDoctorFactory.Create("saeed", "ansari", "2280509504", "brain");
            _context.Manipulate(_ => _.Add(firstDoctor));

            var patient = CreatePatientFactory.Create("saeed", "ansari", "2280509504");
            _context.Manipulate(_ => _.Add(patient));

            var secondDoctor = CreatePatientFactory.Create("saeed", "ansari", "2280509504");
            _context.Manipulate(_ => _.Add(secondDoctor));

            var appointment = new Appointment
            {
                DoctorId = firstDoctor.Id,
                PatientId = patient.Id,
                Date = new DateTime(2022, 04, 27)
            };
            _context.Manipulate(_ => _.Add(appointment));

            var dto = new UpdateAppointmentDto
            {
                DoctorId = secondDoctor.Id,
                PatientId = patient.Id,
                Date = new DateTime(2022, 04, 28)
            };

            _sut.Update(firstDoctor.Id, dto);
            var expected = _context.Appointments.FirstOrDefault();
            expected.DoctorId.Should().Be(dto.DoctorId);
            expected.PatientId.Should().Be(dto.PatientId);
            expected.Date.Should().Be(dto.Date);
        }

        [Fact]
        public void UpdateThrow_Exception_WhenDoctorMoreThanFive_Patient()
        {
            Doctor doctor = CreateDoctorWithFiveAppointmentInOneDay();
            _context.Manipulate(_ => _.Add(doctor));

            var patient = CreatePatientFactory.Create("saeed", "ansari", "2280509504");
            _context.Manipulate(_ => _.Add(patient));

            var dto = new UpdateAppointmentDto
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                Date = DateTime.Now.Date
            };

            Action expected = () => _sut.Update(doctor.Id, dto);

            expected.Should().ThrowExactly<DoctorCannotgreaterthanpermittedGetAppointmentException>();

        }

        [Fact]
        public void UpdateThrow_Exception_WhenPatient_DoesNot_Morethanof_Two()
        {
            var doctor = CreateDoctorFactory.Create("Saeed", "Ansari",
                "2280509504", "Brain");
            _context.Manipulate(_ => _.Doctors.AddRange(doctor));
            var patient = CreatePatientFactory.Create("Saeed", "Ansari",
                "2280509504");
            _context.Manipulate(_ => _.Patients.AddRange(patient));

            var doctorFakeId = 234;

            var appointment = new Appointment
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                Date = new DateTime(2022, 04, 27)
            };
            _context.Manipulate(_ => _.Add(appointment));

            var dto = new UpdateAppointmentDto
            {
                DoctorId = doctorFakeId,
                PatientId = patient.Id,
                Date = new DateTime(2022, 04, 28)
            };

            Action expected = () => _sut.Update(dto.DoctorId, dto);
            expected.Should().ThrowExactly<DoctorDoesNotChangeException>();
        }


        
        
        private static IList<Appointment> CreateListAppointment()
        {
            return new List<Appointment>
            {
                new Appointment
                {
                    DoctorId = 2,
                    PatientId = 3,
                    Date = new DateTime(2022, 04, 26),
                },
                new Appointment
                {
                    DoctorId = 2,
                    PatientId = 3,
                    Date = new DateTime(2022, 04, 26),
                }
            };

        }

        private static IList<Appointment> AddAppointments(int patientId, int doctorId)
        {
            var appointment = new List<Appointment>
            {
                new Appointment
                {
                    DoctorId = doctorId,
                    PatientId = patientId,
                    Date = new DateTime(2022, 04, 26),
                },
                new Appointment
                {
                    DoctorId = doctorId,
                    PatientId = patientId,
                    Date = new DateTime(2022, 04, 27),
                },
                new Appointment
                {
                    DoctorId = doctorId,
                    PatientId = patientId,
                    Date = new DateTime(2022, 04, 28),
                },
                new Appointment
                {
                    DoctorId = doctorId,
                    PatientId = patientId,
                    Date = new DateTime(2022, 04, 29),
                }
            };

            return appointment;

        }


        private static AddAppointmentDto GenerateAddAppointmentDto(int patientId, int doctorId)
        {
            return new AddAppointmentDto
            {
                DoctorId = doctorId,
                PatientId = patientId,
                Date = new DateTime(2022, 04, 30),
            };
        }

        private static UpdateAppointmentDto GenerateUpdateAppointmentDto(int patientId, int doctorId)
        {
            return new UpdateAppointmentDto
            {
                DoctorId = doctorId,
                PatientId = patientId,
                Date = new DateTime(2022, 04, 27),
            };
        }


        public List<Patient> GenerateListOfPatient()
        {
            var patient = new List<Patient>
            {
                new Patient
                {
                    NationalCode = "123",
                    FirstName = "Ali",
                    LastName = "mohammadi"
                },
                new Patient
                {
                    NationalCode = "321",
                    FirstName = "Ali",
                    LastName = "Reza"
                },
                new Patient
                {
                    NationalCode = "465",
                    FirstName = "Ali",
                    LastName = "mohammadi"
                },
                new Patient
                {
                    NationalCode = "321",
                    FirstName = "Ali",
                    LastName = "Reza"
                },
                new Patient
                {
                    NationalCode = "465",
                    FirstName = "Ali",
                    LastName = "mohammadi"
                }
            };

            return patient;
        }

    }
}
