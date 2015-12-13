using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.Hospitals;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.Services.Hospitals
{
    public class HospitalManager : IHospitalManager
    {
        public IHospitalFacade IHospitalFacade { get; set; }
        Func<Owned<IHaslContext>> IHaslContext { get; set; }

        public HospitalManager(Func<Owned<IHaslContext>> iHaslContext, IHospitalFacade iHospitalFacade)
        {
            IHaslContext = iHaslContext;
            IHospitalFacade = iHospitalFacade;
        }



        public async Task<Hospital> FindRegisterdHospital(string userName)
        {
            Hospital registeredHospital = null;
            using (var iHaslContext = IHaslContext())
            {
                registeredHospital = await IHospitalFacade.GetHospitalDetails(iHaslContext.Value, userName);
            }


            return registeredHospital;

        }

        public async Task<int> RegisterNewHospitalOrUpdateDetails(Hospital registeredHospital)
        {
            int modifiedCount = -1;
            using (var iHaslContext = IHaslContext())
            {
                Hospital user = await IHospitalFacade.GetHospitalDetails(iHaslContext.Value, registeredHospital.Email);
                if (user == null)
                {
                    modifiedCount = await IHospitalFacade.RegisterNewHospital(iHaslContext.Value, registeredHospital);

                }
                else
                {
                    modifiedCount = await IHospitalFacade.UpdateHospital(iHaslContext.Value, registeredHospital);

                }
            }
            return modifiedCount;
        }

    }
}
