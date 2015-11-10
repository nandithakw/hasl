using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http.Filters;

namespace Xcendent.Auth.Filteres
{
    public class SecurityExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is SecurityException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            base.OnException(actionExecutedContext);
        }
    }
}