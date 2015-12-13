using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xcendant.HASL.Entities;


namespace Xcendant.HASL.DataAccess.Hospitals
{
    public class HospitalFacade : IHospitalFacade
    {
        public async Task<Hospital> GetHospitalDetails(IHaslContext iHaslContext, string email)
        {
            var hospital = await (from x in iHaslContext.Hospitals
                                  where email.Equals(x.Email)
                                  select x).FirstOrDefaultAsync<Hospital>();
            return hospital;
        }

        public async Task<int> RegisterNewHospital(IHaslContext iHaslContext, Hospital hospital)
        {
            iHaslContext.Hospitals.Add(hospital);
            iHaslContext.Entry(hospital).State = EntityState.Added;
            return await iHaslContext.SaveChangesAsync();
        }

        public async Task<int> UpdateHospital(IHaslContext iHaslContext, Hospital hospital)
        {
            iHaslContext.Entry(hospital).State = EntityState.Modified;
            return await iHaslContext.SaveChangesAsync();
        }
        public async Task<int> DeleteHospital(IHaslContext iHaslContext, Hospital hospital)
        {
            iHaslContext.Entry(hospital).State = EntityState.Modified;
            return await iHaslContext.SaveChangesAsync();
        }
    }
}
