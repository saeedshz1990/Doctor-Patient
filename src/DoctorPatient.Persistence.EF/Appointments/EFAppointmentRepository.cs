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
    }
}
