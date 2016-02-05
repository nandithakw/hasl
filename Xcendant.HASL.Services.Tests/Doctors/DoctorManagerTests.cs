using System.Threading.Tasks;
using Autofac;
using NSubstitute;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.Doctors;
using Xcendant.HASL.Entities;
using Xcendant.HASL.Services.Doctors;
using Xunit;

namespace Xcendant.HASL.Services.Tests.Doctors
{
    public class DoctorManagerTests
    {

        [Fact]
        public async Task RegisterNewOrUpdateDetailsAsyncAndShouldReturnOne()
        {
            ContainerBuilder autoFacBuilder = new ContainerBuilder();
            autoFacBuilder.Register(c =>
            {
                var iHaslContext = Substitute.For<IHaslContext>();
                return iHaslContext;
            }).InstancePerDependency();
            autoFacBuilder.Register(c =>
            {
                var iEntityFacade = Substitute.For<IDoctorFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>()).Returns((Doctor)null);
                iEntityFacade.AddNew(Arg.Any<IHaslContext>(), Arg.Any<Doctor>()).Returns(1);

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<IDoctorFacade>().As<IDoctorManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<IDoctorManager>();
            var modifiedCount = await iCurdLogicManager.RegisterNewOrUpdateDetailsAsync(new Doctor()
            {
                RegistraionNumber = "DOC123456",
                HospitalId = 123456,
                RegisteredUserId = 123456,
            });
            Assert.Equal<int>(1, modifiedCount);
        }


        [Fact]
        public async Task RegisterNewOrUpdateDetailsAsyncEditExsitingAndShouldReturnOne()
        {
            ContainerBuilder autoFacBuilder = new ContainerBuilder();
            autoFacBuilder.Register(c =>
            {
                var iHaslContext = Substitute.For<IHaslContext>();
                return iHaslContext;
            }).InstancePerDependency();
            autoFacBuilder.Register(c =>
            {
                var iEntityFacade = Substitute.For<IDoctorFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>()).Returns(new Doctor { RegisteredUserId = 5 });
                iEntityFacade.UpdateAsync(Arg.Any<IHaslContext>(), Arg.Any<Doctor>()).Returns(1);

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<DoctorManager>().As<IDoctorManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<IDoctorManager>();
            var modifiedCount = await iCurdLogicManager.RegisterNewOrUpdateDetailsAsync(new Doctor()
            {
                HospitalId = 456789,
                RegisteredUserId = 5,
            });
            Assert.Equal<int>(1, modifiedCount);
        }

        [Fact]
        public async Task FindAsyncReturnTheRelatedEntity()
        {
            ContainerBuilder autoFacBuilder = new ContainerBuilder();
            autoFacBuilder.Register(c =>
            {
                var iHaslContext = Substitute.For<IHaslContext>();
                return iHaslContext;
            }).InstancePerDependency();
            autoFacBuilder.Register(c =>
            {
                var iEntityFacade = Substitute.For<IDoctorFacade>();
                iEntityFacade.GetDetails(Arg.Any<IHaslContext>(), Arg.Any<string>())
                .Returns(new Doctor
                {
                    HospitalId = 456789,
                    RegisteredUserId = 5,
                });

                return iEntityFacade;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<DoctorManager>().As<IDoctorManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();
            var iCurdLogicManager = iContainer.Resolve<IDoctorManager>();
            var entity = await iCurdLogicManager.FindAsync("professorX@xmen.com");
            Assert.NotNull(entity);
            Assert.Equal<int>(5, entity.RegisteredUserId);
        }
    }
}
