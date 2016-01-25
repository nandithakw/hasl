using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Xcendant.HASL.Entities.Converters;

namespace Xcendant.HASL.Entities
{
    public class RegisteredUser
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string AddressLine01 { get; set; }
        [Required]
        public string AddressLine02 { get; set; }
        [Required]
        public string City { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
        [Phone]
        public string HomeNumber { get; set; }
        [Phone]
        public string WorkNumber { get; set; }
        [Required]
        [Phone]
        public string MobileNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public IdentificationType IdentificaionType { get; set; }
        [Required]
        public string IdentificationNumber { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [JsonConverter(typeof(ISO8601CalendarDateConverter))]
        public DateTime DateOfBirth { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual ProfileImage ProfileImage { get; set; }

        public virtual CareGiver CareGiver { get; set; }
    }
}
