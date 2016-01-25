using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xcendant.HASL.Entities;


namespace Xcendant.HASL.DataAccess.Hospitals
{
    public class HospitalFacade : AbstractGenericFacade<Hospital>, IHospitalFacade
    {
        public override async Task<Hospital> GetDetails<U>(IHaslContext iHaslContext, U key)
        {
            var hospital = await (from x in iHaslContext.Hospitals
                                  where key.Equals(x.Email)
                                  select x).FirstOrDefaultAsync();
            return hospital;
        }





    }
}
