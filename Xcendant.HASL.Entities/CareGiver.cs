using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xcendant.HASL.Entities
{
    public class CareGiver
    {
        [Key, ForeignKey("RegisteredUser")]
        public int RegisteredUserId { get; set; }
        public string FullName
        {
            get { return this.RegisteredUser != null ? this.RegisteredUser.FirstName + this.RegisteredUser.LastName : null; }
        }

        public string Id
        {
            get { return "CT" + this.RegisteredUserId; }
        }
        public string Description { get; set; }

        public virtual RegisteredUser RegisteredUser { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

    }
}
