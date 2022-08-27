using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Javno
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            // See http://stackoverflow.com/questions/13905164/how-to-make-custom-error-pages-work-in-asp-net-mvc-4
            // for additional context on use of this technique

            var exception = Server.GetLastError();
            if (exception != null)
            {
                // This would be a good place to log any relevant details about the exception.
                // Since we are going to pass exception information to our error page via querystring,
                // it will only be practical to issue a short message. Further detail would have to be logged somewhere.

                // This will invoke our error page, passing the exception message via querystring parameter
                // Note that we chose to use Server.TransferRequest, which is only supported in IIS 7 and above.
                // As an alternative, Response.Redirect could be used instead.
                // Server.Transfer does not work (see https://support.microsoft.com/en-us/kb/320439 )
                Server.TransferRequest("~/Error?Message=" + exception.Message);
            }

        }
    }
}
