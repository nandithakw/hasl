using Microsoft.AspNet.Identity.EntityFramework;

namespace Xcendent.Auth.Models.Entities
{
    public class XcendentUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}