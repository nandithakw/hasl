using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xcendant.HASL.Entities
{
    public class Patient
    {
        [Key, ForeignKey("RegisteredUser")]
        public int RegisteredUserId { get; set; }

        public string FullName
        {
            get { return this.RegisteredUser != null ? this.RegisteredUser.FirstName + this.RegisteredUser.LastName : null; }
        }

        public string Id
        {
            get { return "DOC" + this.RegisteredUserId; }
        }
        public string RegistrationNumber { get; set; }

        public HemophiliaType HemophiliaType { get; set; }
        public HemophiliaServity HemophiliaServity { get; set; }
        public int CounsultingDoctorId { get; set; }
        public int CounsultingHospitalId { get; set; }
        public int PersonelCareGiverId { get; set; }
        public int ConusltingTreatmentCenterId { get; set; }

        public virtual RegisteredUser RegisteredUser { get; set; }

        [ForeignKey("CounsultingDoctorId")]
        public virtual Doctor CounsultingDoctor { get; set; }
        [ForeignKey("CounsultingHospitalId")]
        public virtual Hospital CounsultingHospital { get; set; }
        [ForeignKey("PersonelCareGiverId")]
        public virtual CareGiver PersonelCareGiver { get; set; }
        [ForeignKey("ConusltingTreatmentCenterId")]
        public virtual TreatmentCenter ConusltingTreatmentCenter { get; set; }
    }
}
