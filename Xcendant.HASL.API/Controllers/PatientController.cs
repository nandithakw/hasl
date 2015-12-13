using System.Web.Http;

namespace Xcendant.HASL.API.Controllers
{
    [RoutePrefix("api/patient")]
    public class PatientController : ApiController
    {
        [HttpPost]
        [Route("details")]
        public void Getsss(string name)
        {


        }
    }
}
