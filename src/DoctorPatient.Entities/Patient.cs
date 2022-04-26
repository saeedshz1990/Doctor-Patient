using System.Collections.Generic;

namespace DoctorPatient.Entities
{
    public class Patient :EntityBase
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
        }

        public HashSet<Appointment> Appointments { get; set; }
        
    }
}
