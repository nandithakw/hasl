using System;
using Autofac.Features.OwnedInstances;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.TreatmentCenters;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.Services.TreatmentCenters
{
    public class TreatmentCenterManager : AbstractCRUDLogicManager<TreatmentCenter, ITreatmentCenterFacade>, ITreatmentCenterManager
    {

        public TreatmentCenterManager(Func<Owned<IHaslContext>> iHaslContext, ITreatmentCenterFacade iTreatmentCenterFacade) : base(iHaslContext, iTreatmentCenterFacade)
        {

        }

    }
}
