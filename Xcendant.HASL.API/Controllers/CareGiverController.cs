using System.Threading.Tasks;
using System.Web.Http;
using Xcendant.HASL.Services.CareGivers;

namespace Xcendant.HASL.API.Controllers
{
    [RoutePrefix("api/CareGivers")]
    public class CareGiverController : ApiController
    {
        private ICareGiverManager ICareGiverManager { get; set; }

        public CareGiverController(ICareGiverManager iCareGiverManager)
        {
            ICareGiverManager = iCareGiverManager;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            IHttpActionResult result = null;
            try
            {
                var careGivers = await ICareGiverManager.GetAllAsync();
                result = Ok(careGivers);
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
