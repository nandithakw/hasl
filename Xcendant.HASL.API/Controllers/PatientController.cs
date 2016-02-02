using System.Web.Http;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.API.Controllers
{
    [RoutePrefix("api/patient")]
    public class PatientController : ApiController
    {
        public IPatientManager IPatientManager { get; private set; }

        public PatientController(IPatientManager iPatientManager)
        {
            IPatientManager = iPatientManager;
        }

        [Route("{userName}")]
        public async Task<IHttpActionResult> GetAsync(string userName)
        {

            IHttpActionResult result = null;
            try
            {
                Patient registeredUser = await IPatientManager.FindAsync(userName);
                result = Ok(registeredUser);
            }
            catch (Exception ex)
            {
                result = InternalServerError(ex);
            }
            return result;
        }

        [Route("{email}")]
        public async Task<IHttpActionResult> PostAsync(Patient user)
        {

            IHttpActionResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    int modifiedCount = await IPatientManager.RegisterNewOrUpdateDetailsAsync(user);
                    if (modifiedCount > 0)
                    {
                        result = Ok("New user registration succeded");

                    }
                    else
                    {
                        result = Ok("New user registration failed.Please retry.");

                    }
                }
                else
                {
                    result = BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                result = InternalServerError(ex);
            }
            return result;
        }

        [Route("{email}")]
        public async Task<IHttpActionResult> PutAsync(Patient user)
        {

            IHttpActionResult result = null;
            try
            {
                int modifiedCount = await IPatientManager.RegisterNewOrUpdateDetailsAsync(user);
                if (modifiedCount > 0)
                {
                    result = Ok("User details update succeded");

                }
                else
                {
                    result = Ok("User details update failed.Please retry.");

                }
            }
            catch (Exception ex)
            {
                result = InternalServerError(ex);
            }
            return result;
        }
    }
}
