using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Javno.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string Message)
        {
            // We choose to use the ViewBag to communicate the error message to the view
            ViewBag.Message = Message;
            return View();
        }
        public ActionResult ErrorCode(int id)
        {
            Response.StatusCode = id;
            return View();
        }
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}