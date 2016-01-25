using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xcendant.HASL.Entities
{
    public class CareGiver
    {
        public int Id { get; set; }
        [Key, ForeignKey("RegisteredUser")]
        public int RegisteredUserId { get; set; }
        public string Description { get; set; }

        public virtual RegisteredUser RegisteredUser { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

    }
}
