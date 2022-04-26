using System.Collections.Generic;
using DoctorPatient.Services.Doctors.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPatient.RestAPI.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorsController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpPost]
        public void AddDoctor(AddDoctorDto dto)
        {
            _doctorService.Add(dto);
        }

        [HttpGet]
        public IList<GetDoctorDto> GetAll()
        {
            return _doctorService.GetAll();
        }


        [HttpPut("{id}")]
        public void Update([FromRoute] int id, [FromBody] UpdateDoctorDto dto)
        {
            _doctorService.Update(dto,id);
        }
    }
}
