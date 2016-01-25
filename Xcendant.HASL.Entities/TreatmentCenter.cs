using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xcendant.HASL.Entities
{
    public class TreatmentCenter
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Telephone { get; set; }

        [Phone]
        public string Mobile { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string AddressLine01 { get; set; }
        [Required]
        public string AddressLine02 { get; set; }


        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string District { get; set; }


        [Required]
        public string Country { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }

    }
}
