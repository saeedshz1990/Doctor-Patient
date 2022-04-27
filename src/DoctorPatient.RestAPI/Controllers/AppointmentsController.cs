using System.Collections.Generic;
using DoctorPatient.Services.Appointments.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPatient.RestAPI.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _service;

        public AppointmentsController(AppointmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddAppointmentDto dto)
        {
            _service.Add(dto);
        }

        [HttpGet]
        public List<GetAppointmentDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpPut("{id}")]
        public void Update([FromRoute] int id, [FromBody] UpdateAppointmentDto dto)
        {
            _service.Update(id, dto);
        }

    }
}
