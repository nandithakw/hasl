using System;
using Autofac.Features.OwnedInstances;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.CareGivers;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.Services.CareGivers
{
    public class CareGiverManager : AbstractCRUDLogicManager<CareGiver, ICareGiverFacade>, ICareGiverManager
    {
        public CareGiverManager(Func<Owned<IHaslContext>> iHaslContext, ICareGiverFacade iCareGiverFacade)
            : base(iHaslContext, iCareGiverFacade)
        {


        }

    }
}
