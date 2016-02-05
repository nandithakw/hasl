using System;
using System.Threading.Tasks;
using System.Web.Http;
using Xcendant.HASL.Entities;
using Xcendant.HASL.Services.Hospitals;

namespace Xcendant.HASL.API.Controllers
{
    [RoutePrefix("api/Hospitals")]
    public class HospitalController : ApiController
    {
        public IHospitalManager IHospitalManager { get; private set; }

        public HospitalController(IHospitalManager iHospitalManager)
        {
            IHospitalManager = iHospitalManager;
        }


        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            IHttpActionResult result = null;
            try
            {
                var hospitals = await IHospitalManager.GetAllAsync();
                result = Ok(hospitals);
            }
            catch (System.Exception ex)
            {
                result = InternalServerError(ex);
                throw;
            }
            return result;
        }

        [HttpPost]
        [Route("details")]
        public async Task<IHttpActionResult> GetHospitalDetails([FromBody]string userName)
        {

            IHttpActionResult result = null;
            try
            {
                Hospital registeredHospital = await IHospitalManager.FindAsync(userName);
                result = Ok(registeredHospital);
            }
            catch (Exception ex)
            {
                result = InternalServerError(ex);
            }
            return result;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> RegisterNewHospital(Hospital user)
        {

            IHttpActionResult result = null;
            try
            {
                int modifiedCount = await IHospitalManager.RegisterNewOrUpdateDetailsAsync(user);
                if (modifiedCount > 0)
                {
                    result = Ok("New user registration succeded");

                }
                else
                {
                    result = Ok("New user registration failed.Please retry.");

                }
            }
            catch (Exception ex)
            {
                result = InternalServerError(ex);
            }
            return result;
        }

        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> UpdateHospital(Hospital user)
        {

            IHttpActionResult result = null;
            try
            {
                int modifiedCount = await IHospitalManager.RegisterNewOrUpdateDetailsAsync(user);
                if (modifiedCount > 0)
                {
                    result = Ok("Hospital details update succeded");

                }
                else
                {
                    result = Ok("Hospital details update failed.Please retry.");

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
