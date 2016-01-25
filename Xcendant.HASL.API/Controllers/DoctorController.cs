using System.Threading.Tasks;
using System.Web.Http;
using Xcendant.HASL.Services.Doctors;

namespace Xcendant.HASL.API.Controllers
{
    [RoutePrefix("api/Doctors")]
    public class DoctorController : ApiController
    {
        private IDoctorManager IDoctorManager { get; set; }

        public DoctorController(IDoctorManager iDoctorManager)
        {
            IDoctorManager = iDoctorManager;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            IHttpActionResult result = null;
            try
            {
                var doctors = await IDoctorManager.GetAllAsync();
                result = Ok(doctors);
            }
            catch (System.Exception ex)
            {
                result = InternalServerError(ex);
                throw;
            }
            return result;
        }
    }
}
