using System;
using System.Collections.Generic;
using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;
using DoctorPatient.Infrastructure.Test;
using DoctorPatient.Persistence.EF;
using DoctorPatient.Persistence.EF.Patients;
using DoctorPatient.Services.Doctors.Exceptions;
using DoctorPatient.Services.Patients;
using DoctorPatient.Services.Patients.Contracts;
using DoctorPatient.Services.Patients.Exceptions;
using DoctorPatient.Test.Tools.Patients;
using FluentAssertions;
using Xunit;

namespace DoctorPatient.Services.Test.Unit.Patients
{
    public class PatientServiceTest
    {
        private readonly EFDataContext _context;
        private readonly PatientService _sut;
        private readonly PatientRepository _patientRepository;
        private readonly UnitOfWork _unitOfWork;

        public PatientServiceTest()
        {
            _context = new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _patientRepository = new EFPatientRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new PatientAppService(_unitOfWork, _patientRepository);
        }

        [Fact]
        public void Add_adds_Patients_Properly()
        {
            AddPatientDto dto = GenerateAddPatientDto();
            var patient = CreateListPatient();

            _context.Manipulate(_ =>
                _.Patients.AddRange(patient));

            _sut.Add(dto);
            _context.Patients.Should()
                .Contain(_ => _.NationalCode == dto.NationalCode);
        }

        [Fact]
        public void AddThrow_DoctorIsExist_When_NationalCode_IsExist()
        {
            var patient = CreatePatientFactory.Create("Saeed", "Ansari",
                "2280509504");
            _context.Manipulate(_ => _.Patients.Add(patient));

            AddPatientDto dto = new AddPatientDto
            {
                FirstName = "Saeed",
                LastName = "Ansari",
                NationalCode = "2280509504"
               
            };

            Action expected = () => _sut.Add(dto);
            expected.Should().ThrowExactly<PatientNationalCodeExistException>();
        }



        private static List<Patient> CreateListPatient()
        {
            var patient = new List<Patient>
            {
                new Patient
                {
                    NationalCode = "2280509504",
                    FirstName = "Ali",
                    LastName = "mohammadi"
                },
                new Patient
                {
                    NationalCode = "2280509504",
                    FirstName = "Ali",
                    LastName = "Reza"
                }
            };

            return patient;

        }

        private static AddPatientDto GenerateAddPatientDto()
        {
            return new AddPatientDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                NationalCode = "NationalCode",
            };
        }
    }
}
