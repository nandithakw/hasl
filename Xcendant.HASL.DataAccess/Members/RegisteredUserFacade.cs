using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess.Members
{
    public class RegisteredUserFacade : IRegisteredUserFacade
    {
        public async Task<RegisteredUser> GetUserDetails(IHaslContext iHaslContext, string email)
        {
            var registerdUser = await (from x in iHaslContext.RegisteredUsers
                                       .Include("ProfileImage")
                                       where email.Equals(x.Email)
                                       select x).FirstOrDefaultAsync<RegisteredUser>();
            return registerdUser;
        }

        public async Task<int> RegisterNewUserDetails(IHaslContext iHaslContext, RegisteredUser userDetails)
        {
            iHaslContext.RegisteredUsers.Add(userDetails);
            iHaslContext.Entry(userDetails).State = EntityState.Added;
            return await iHaslContext.SaveChangesAsync();
        }

        public async Task<int> UpdateUserDetails(IHaslContext iHaslContext, RegisteredUser userDetails)
        {
            iHaslContext.Entry(userDetails).State = EntityState.Modified;
            return await iHaslContext.SaveChangesAsync();
        }
    }
}
