using System;
using System.Collections.Generic;
using DoctorPatient.Entities;
using DoctorPatient.Infrastructure.Application;
using DoctorPatient.Infrastructure.Test;
using DoctorPatient.Persistence.EF;
using DoctorPatient.Persistence.EF.Doctors;
using DoctorPatient.Services.Doctors;
using DoctorPatient.Services.Doctors.Contracts;
using DoctorPatient.Services.Doctors.Exceptions;
using DoctorPatient.Test.Tools.Doctor;
using FluentAssertions;
using Xunit;

namespace DoctorPatient.Services.Test.Unit.Doctors
{
    public class DoctorServiceTest
    {
        private readonly EFDataContext _context;
        private readonly DoctorService _sut;
        private readonly DoctorRepository _doctorRepository;
        private readonly UnitOfWork _unitOfWork;

        public DoctorServiceTest()
        {
            _context = new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _doctorRepository = new EFDoctorRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new DoctorAppService(_unitOfWork, _doctorRepository);
        }

        [Fact]
        public void Add_adds_Doctor_Properly()
        {
            AddDoctorDto dto = GenerateAddDoctorDto();
            var doctor = CreateListDoctor();

            _context.Manipulate(_ =>
                _.Doctors.AddRange(doctor));

            _sut.Add(dto);
            _context.Doctors.Should()
                .Contain(_ => _.NationalCode == dto.NationalCode);

        }

        [Fact]
        public void AddThrow_DoctorIsExist_When_NationalCode_IsExist()
        {
            var doctor = CreateDoctorFactory.Create("Saeed", "Ansari",
                 "2280509504", "Programmer");
            _context.Manipulate(_ => _.Doctors.Add(doctor));

            AddDoctorDto dto = new AddDoctorDto
            {
                FirstName = "Saeed",
                LastName = "Ansari",
                NationalCode = "2280509504",
                Field = "Programmer"
            };

            Action expected = () => _sut.Add(dto);
            expected.Should().ThrowExactly<DoctorNationalCodeExistException>();
        }

        [Fact]
        public void GetAll_Doctors_return_All()
        {
            var doctor = CreateDoctorFactory.Create("Saeed", "Ansari",
                "2280509504", "Programmer");

            _context.Manipulate(_ =>
                _.Doctors.AddRange(doctor));

            var expected = _sut.GetAll();

            expected.Should().HaveCount(1);
            expected.Should().Contain(_ => _.NationalCode == "2280509504");
        }

        [Fact]
        public void Update_updates_Doctors_Properly()
        {
            var doctor = CreateDoctorFactory.Create("Saeed", "Ansari",
                "2480509504", "Programmer");

            _context.Manipulate(_ =>
                _.Doctors.Add(doctor));

            var dto = new UpdateDoctorDto
            {
                FirstName = "Saeed",
                LastName = "Ansari",
                NationalCode = "2280506504",
                Field = "Programmer"
            };

            _sut.Update(dto, doctor.Id);

            _context.Doctors.Should()
                .Contain(_ => _.NationalCode == "2280506504");
        }

        [Fact]
        public void UpdateThrow_DoctorWithThisIdDoesNotExistException_if_Doctor_Doesnot_Exist()
        {
            var testDoctorid = 4152;

            var doctor = CreateDoctorFactory.Create("Saeed", "Ansari",
                "2280509504", "Programmer");
            _context.Manipulate(x => x.Doctors.Add(doctor));

            UpdateDoctorDto dto = new UpdateDoctorDto
            {
                Id = 1,
                FirstName = "Saeed",
                LastName = "Ansari",
                NationalCode = "2280509504",
                Field = "Programmer"
            };

            Action expected = () => _sut.Update(dto, doctor.Id);
            expected.Should().ThrowExactly<DoctorNationalCodeIsExistsInDatabase>();

        }

        [Fact]
        public void Delete_deletes_Doctor_Properly()
        {
            var doctor = CreateDoctorFactory.Create("Saeed", "Ansari",
                "2280509504", "Programmer");

            _context.Manipulate(_ =>
                _doctorRepository.Add(doctor));
            _sut.Delete(doctor.Id);
            _context.Doctors
                .Should()
                .HaveCount(0);
        }

        [Fact]
        public void DeleteThrow_DoctorId_IsNotExistInDatabse()
        {
            var testDoctorid = 4152;
            var doctor = CreateDoctorFactory.Create("Saeed", "Ansari",
                "2280509504", "Programmer");

            _context.Manipulate(_ => _.Doctors.Add(doctor));
            Action expected = () => _sut.Delete(testDoctorid);
            expected.Should().ThrowExactly<DoctorIdDoesNotExistException>();


        }

        private static UpdateDoctorDto GenerateUpdateDoctorDto()
        {
            return new UpdateDoctorDto
            {
                Field = "Brain",
                FirstName = "Saeed",
                LastName = "Ansari",
                NationalCode = "2280509504",
                Id = 1

            };

        }

        private static List<Doctor> CreateListDoctor()
        {
            var doctor = new List<Doctor>
            {
                new Doctor
                {
                    NationalCode = "2280509504",
                    FirstName = "Ali",
                    LastName = "mohammadi",
                    Field = "jarah",
                },
                new Doctor
                {
                    NationalCode = "2280509504",
                    FirstName = "Ali",
                    LastName = "Reza",
                    Field = "Field",
                }
            };

            return doctor;
        }

        private static AddDoctorDto GenerateAddDoctorDto()
        {
            return new AddDoctorDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                NationalCode = "NationalCode",
                Field = "Field",
            };
        }
    }
}

