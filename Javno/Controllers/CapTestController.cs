using Newtonsoft.Json.Linq;
using Recaptcha.Web.Mvc;
using RWADatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Javno.Controllers
{
    public class CapTestController : Controller
    {
        // GET: CapTest
        // GET: Apartment
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ApartmentReservation model)
        {
            var recaptchaHelper = this.GetRecaptchaVerificationHelper();
            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError(
                    "",
                    "Captcha answer cannot be empty.");
                return View(model);
            }

            var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            if (!recaptchaResult.Success)
            {
                ModelState.AddModelError(
                    "",
                    "Incorrect captcha answer.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }
       
        [HttpPost]
        public JsonResult AjaxMethod(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + "6LfI9XYhAAAAAAU7-ys6LzivfAEiWae_ojzthjtT" + "&response=" + response;
            string responseStr = (new WebClient()).DownloadString(url);
            return Json(responseStr);
        }

        public ActionResult Index3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitForm()
        {
            //To Validate Google recaptcha
            var response = Request["g-recaptcha-response"];
            string secretKey = "6LfI9XYhAAAAAAU7-ys6LzivfAEiWae_ojzthjtT";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");

            //check the status is true or not
            if (status == true)
            {
                ViewBag.Message = "Your Google reCaptcha validation success";
            }
            else
            {
                ViewBag.Message = "Your Google reCaptcha validation failed";
            }

            return View("Index");
        }



    }
}