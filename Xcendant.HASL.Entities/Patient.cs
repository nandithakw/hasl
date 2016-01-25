using System.ComponentModel.DataAnnotations.Schema;

namespace Xcendant.HASL.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }

        public HemophiliaType HemophiliaType { get; set; }
        public HemophiliaServity HemophiliaServity { get; set; }
        public int CounsultingDoctorId { get; set; }
        public int CounsultingHospitalId { get; set; }
        public int PersonelCareGiverId { get; set; }
        public int ConusltingTreatmentCenterId { get; set; }


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
