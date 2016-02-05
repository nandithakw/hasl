using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess.Patients
{
    public class PatientFacade : AbstractGenericFacade<Patient>, IPatientFacade
    {
        public override async Task<Patient> GetDetails<U>(IHaslContext iHaslContext, U key)
        {
            var registerdUser = await (from x in iHaslContext.Patients
                                           //.Include("ProfileImage")
                                       where key.Equals(x.RegistrationNumber)
                                       select x).AsNoTracking().FirstOrDefaultAsync<Patient>();
            return registerdUser;
        }




    }
}

