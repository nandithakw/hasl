using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.Hospitals;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.Services.Hospitals
{
    public class HospitalManager : AbstractCRUDLogicManager<Hospital, IHospitalFacade>, IHospitalManager
    {


        public HospitalManager(Func<Owned<IHaslContext>> iHaslContext, IHospitalFacade iHospitalFacade)
            : base(iHaslContext, iHospitalFacade)
        {

        }




        public async Task<int> RegisterNewOrUpdateDetailsAsync(Hospital registeredHospital)
        {
            int modifiedCount = -1;
            using (var ctx = iHaslContext())
            {
                Hospital user = await iEntityFacade.GetDetails(ctx.Value, registeredHospital.Email);
                if (user == null)
                {
                    modifiedCount = await iEntityFacade.AddNew(ctx.Value, registeredHospital);

                }
                else
                {
                    modifiedCount = await iEntityFacade.UpdateAsync(ctx.Value, registeredHospital);

                }
            }
            return modifiedCount;
        }

    }
}
