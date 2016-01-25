using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess.Doctors
{
    public class DoctorFacade : AbstractGenericFacade<Doctor>, IDoctorFacade
    {


        public override async Task<Doctor> GetDetails<U>(IHaslContext iHaslContext, U key)
        {
            var doctor = await (from x in iHaslContext.Doctors
                                 .Include("RegisteredUser")
                                where key.Equals(x.RegisteredUser.Email)
                                select x).AsNoTracking().FirstOrDefaultAsync<Doctor>();
            return doctor;
        }
    }
}
