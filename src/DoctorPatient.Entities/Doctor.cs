using System.Collections.Generic;

namespace DoctorPatient.Entities
{
    public class Doctor :EntityBase
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
        }
        public string Field { get; set; }
        public HashSet<Appointment> Appointments { get; set; }
    }
}
