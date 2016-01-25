using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xcendant.HASL.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        [Key, ForeignKey("RegisteredUser")]
        public int RegisteredUserId { get; set; }
        [Required]
        public string RegistraionNumber { get; set; }
        public int HospitalId { get; set; }

        public virtual RegisteredUser RegisteredUser { get; set; }
        public virtual Hospital ConsultingHospital { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }



    }
}
