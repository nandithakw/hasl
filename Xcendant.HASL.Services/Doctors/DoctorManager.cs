using System;
using Autofac.Features.OwnedInstances;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.Doctors;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.Services.Doctors
{
    public class DoctorManager : AbstractCRUDLogicManager<Doctor, IDoctorFacade>, IDoctorManager
    {

        public DoctorManager(Func<Owned<IHaslContext>> iHaslContext, IDoctorFacade iDoctorFacade)
            : base(iHaslContext, iDoctorFacade)
        {

        }

    }
}
