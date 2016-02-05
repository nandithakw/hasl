using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Autofac;
using NSubstitute;
using Xcendant.HASL.API.Controllers;
using Xcendant.HASL.Entities;
using Xcendant.HASL.Services.Patients;
using Xunit;

namespace Xcendant.HASL.API.Tests.Controllers
{
    public class PatientControllerTests
    {
        [Fact]
        public async Task GetPatientDetailsShouldReturnOKWithPatient()
        {


            ContainerBuilder autoFacBuilder = new ContainerBuilder();

            autoFacBuilder.Register(c =>
            {
                var iPatientManager = Substitute.For<PatientManager>();
                iPatientManager.FindAsync(Arg.Any<string>()).Returns<Patient>(new Patient { RegisteredUserId = 123 });
                return iPatientManager;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<PatientManager>().As<PatientManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();

            PatientController controller = new PatientController(iContainer.Resolve<IPatientManager>());
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            IHttpActionResult actionResult = await controller.GetAsync("professorX@smen.com");

            var contentResult = actionResult as OkNegotiatedContentResult<Patient>;
            Assert.NotNull(contentResult);
            Assert.Equal(123, contentResult.Content.RegisteredUserId);

        }


        [Fact]
        public async Task RegisterNewPatientShouldReturnOK()
        {


            ContainerBuilder autoFacBuilder = new ContainerBuilder();

            autoFacBuilder.Register(c =>
            {
                var iPatientManager = Substitute.For<PatientManager>();
                iPatientManager.RegisterNewOrUpdateDetailsAsync(Arg.Any<Patient>()).Returns<int>(1);
                return iPatientManager;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<PatientManager>().As<PatientManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();

            PatientController controller = new PatientController(iContainer.Resolve<IPatientManager>());
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            IHttpActionResult actionResult = await controller.PostAsync(new Patient());

            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            Assert.NotNull(contentResult);
            Assert.Equal("New user registration succeded", contentResult.Content);

        }



        [Fact]
        public async Task UpdatePatientShouldReturnOK()
        {


            ContainerBuilder autoFacBuilder = new ContainerBuilder();

            autoFacBuilder.Register(c =>
            {
                var iPatientManager = Substitute.For<PatientManager>();
                iPatientManager.RegisterNewOrUpdateDetailsAsync(Arg.Any<Patient>()).Returns<int>(1);
                return iPatientManager;
            }).InstancePerDependency();
            autoFacBuilder.RegisterType<PatientManager>().As<PatientManager>().InstancePerDependency();


            IContainer iContainer = autoFacBuilder.Build();

            PatientController controller = new PatientController(iContainer.Resolve<IPatientManager>());
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            IHttpActionResult actionResult = await controller.PutAsync(new Patient());

            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            Assert.NotNull(contentResult);
            Assert.Equal("Patient details update succeded", contentResult.Content);

        }
    }
}
