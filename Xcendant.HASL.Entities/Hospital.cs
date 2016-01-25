using System;
using System.Collections.Generic;

namespace Xcendant.HASL.Entities
{
    public class Hospital
    {
        public int Id { get; set; }
        public string HospitalId { get { return String.Format("HT{0:0000}", Id); } }
        public string Name { get; set; }
        public string AdressLine01 { get; set; }
        public string AddressLine02 { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }


    }
}
