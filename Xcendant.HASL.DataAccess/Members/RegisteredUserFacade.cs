using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess.Members
{
    public class RegisteredUserFacade : AbstractGenericFacade<RegisteredUser>, IRegisteredUserFacade
    {
        public override async Task<RegisteredUser> GetDetails<U>(IHaslContext iHaslContext, U key)
        {
            var registerdUser = await (from x in iHaslContext.RegisteredUsers
                                      .Include("ProfileImage")
                                       where key.Equals(x.Email)
                                       select x).AsNoTracking().FirstOrDefaultAsync<RegisteredUser>();
            return registerdUser;
        }




    }
}
