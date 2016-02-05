using System.Threading.Tasks;
using System.Web.Http;
using Xcendant.HASL.Services.TreatmentCenters;

namespace Xcendant.HASL.API.Controllers
{
    [RoutePrefix("api/TreatmentCenters")]
    public class TreatmentCenterController : ApiController
    {
        private ITreatmentCenterManager ITreatmentCenterManager { get; set; }

        public TreatmentCenterController(ITreatmentCenterManager iTreatmentCenterManager)
        {
            ITreatmentCenterManager = iTreatmentCenterManager;
        }

        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            IHttpActionResult result = null;
            try
            {
                var treatmentCenters = await ITreatmentCenterManager.GetAllAsync();
                result = Ok(treatmentCenters);
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
