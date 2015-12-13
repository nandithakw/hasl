namespace Xcendant.HASL.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public Doctor CounsultingDoctor { get; set; }
        public Hospital CounsultingHospital { get; set; }
        public CareGiver PersonelCareGiver { get; set; }
        public TreatmentCenter ConusltingTreatmentCenter { get; set; }
    }
}
