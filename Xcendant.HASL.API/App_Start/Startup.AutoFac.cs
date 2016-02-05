using Autofac;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.CareGivers;
using Xcendant.HASL.DataAccess.Doctors;
using Xcendant.HASL.DataAccess.Hospitals;
using Xcendant.HASL.DataAccess.Members;
using Xcendant.HASL.DataAccess.Patients;
using Xcendant.HASL.DataAccess.TreatmentCenters;
using Xcendant.HASL.Services.CareGivers;
using Xcendant.HASL.Services.Doctors;
using Xcendant.HASL.Services.Hospitals;
using Xcendant.HASL.Services.Members;
using Xcendant.HASL.Services.Patients;
using Xcendant.HASL.Services.TreatmentCenters;

namespace Xcendant.HASL.API
{
    public partial class Startup
    {
        public void ConfigureAutoFac(ContainerBuilder autoFacBuilder)
        {
            autoFacBuilder.RegisterType<HaslContext>().As<IHaslContext>().InstancePerDependency();
            autoFacBuilder.RegisterType<RegisteredUserFacade>().As<IRegisteredUserFacade>().InstancePerRequest();
            autoFacBuilder.RegisterType<RegisteredUserManager>().As<IRegisteredUserManager>().InstancePerRequest();
            autoFacBuilder.RegisterType<PatientManager>().As<IPatientManager>().InstancePerRequest();
            autoFacBuilder.RegisterType<PatientFacade>().As<IPatientFacade>().InstancePerRequest();
            autoFacBuilder.RegisterType<DoctorManager>().As<IDoctorManager>().InstancePerRequest();
            autoFacBuilder.RegisterType<DoctorFacade>().As<IDoctorFacade>().InstancePerRequest();
            autoFacBuilder.RegisterType<HospitalManager>().As<IHospitalManager>().InstancePerRequest();
            autoFacBuilder.RegisterType<HospitalFacade>().As<IHospitalFacade>().InstancePerRequest();
            autoFacBuilder.RegisterType<CareGiverManager>().As<ICareGiverManager>().InstancePerRequest();
            autoFacBuilder.RegisterType<CareGiverFacade>().As<ICareGiverFacade>().InstancePerRequest();
            autoFacBuilder.RegisterType<TreatmentCenterManager>().As<ITreatmentCenterManager>().InstancePerRequest();
            autoFacBuilder.RegisterType<TreatmentCenterFacade>().As<ITreatmentCenterFacade>().InstancePerRequest();



        }
    }
}