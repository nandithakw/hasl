using System;
using System.Threading.Tasks;
using System.Web.Http;
using Xcendant.HASL.Entities;
using Xcendant.HASL.Services.Members;
namespace Xcendant.HASL.API.Controllers
{
    [RoutePrefix("api/RegisteredUser")]
    public class RegisteredUserController : ApiController
    {
        public IRegisteredUserManager IRegisteredUserManager { get; private set; }

        public RegisteredUserController(IRegisteredUserManager iRegisteredUserManager)
        {
            IRegisteredUserManager = iRegisteredUserManager;
        }

        [Route("{userName}")]
        public async Task<IHttpActionResult> GetAsync(string userName)
        {

            IHttpActionResult result = null;
            try
            {
                RegisteredUser registeredUser = await IRegisteredUserManager.FindAsync(userName);
                result = Ok(registeredUser);
            }
            catch (Exception ex)
            {
                result = InternalServerError(ex);
            }
            return result;
        }

        [Route("{email}")]
        public async Task<IHttpActionResult> PostAsync(RegisteredUser user)
        {

            IHttpActionResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    int modifiedCount = await IRegisteredUserManager.RegisterNewOrUpdateDetailsAsync(user);
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
        public async Task<IHttpActionResult> PutAsync(RegisteredUser user)
        {

            IHttpActionResult result = null;
            try
            {
                int modifiedCount = await IRegisteredUserManager.RegisterNewOrUpdateDetailsAsync(user);
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
