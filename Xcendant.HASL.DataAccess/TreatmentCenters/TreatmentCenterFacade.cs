using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess.TreatmentCenters
{
    public class TreatmentCenterFacade : AbstractGenericFacade<TreatmentCenter>, ITreatmentCenterFacade
    {


        public override async Task<TreatmentCenter> GetDetails<U>(IHaslContext iHaslContext, U key)
        {
            var doctor = await (from x in iHaslContext.TreatmentCenters
                                where key.Equals(x.Email)
                                select x).AsNoTracking().FirstOrDefaultAsync();
            return doctor;
        }
    }
}
