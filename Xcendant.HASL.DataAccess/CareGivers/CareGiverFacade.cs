using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess.CareGivers
{
    public class CareGiverFacade : AbstractGenericFacade<CareGiver>, ICareGiverFacade
    {


        public override async Task<CareGiver> GetDetails<U>(IHaslContext iHaslContext, U key)
        {
            var careGiver = await (from x in iHaslContext.CareGivers
                                      .Include("RegisteredUser")
                                   where key.Equals(x.RegisteredUser.Email)
                                   select x).AsNoTracking().FirstOrDefaultAsync<CareGiver>();
            return careGiver;
        }
    }
}
