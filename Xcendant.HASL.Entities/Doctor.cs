namespace Xcendant.HASL.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegistraionNumber { get; set; }
        public Hospital ConsultingHospital { get; set; }

    }
}
