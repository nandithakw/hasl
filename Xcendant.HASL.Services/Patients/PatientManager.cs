using System;
using Autofac.Features.OwnedInstances;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.Patients;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.Services.Patients
{
    public class PatientManager : AbstractCRUDLogicManager<Patient, IPatientFacade>, IPatientManager
    {

        public PatientManager(Func<Owned<IHaslContext>> iHaslContext, IPatientFacade iPatientFacade) : base(iHaslContext, iPatientFacade)
        {

        }
    }
}
