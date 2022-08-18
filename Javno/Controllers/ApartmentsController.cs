using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Javno.Data;
using Javno.Models.ViewModels;
using RWADatabaseLibrary.Models;
using RWADatabaseLibrary.Models.ViewModels;
using RWADatabaseLibrary.Repository;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.IO;

namespace Javno.Controllers
{
    public class ApartmentsController : Controller
    {
        private ApartmentRepository _apartmentRepository = new ApartmentRepository();
        private ApartmentReservationRepository _apartmentReservationRepository = new ApartmentReservationRepository();
        private CityRepository _cityRepository = new CityRepository();
        private UserRepository _userRepository = new UserRepository();

        private bool firstTimeUse = false;
        // GET: Apartments
        public ActionResult Index()
        {
            SearchFiltersViewModel searchFiltersViewModel = new SearchFiltersViewModel();
            searchFiltersViewModel.Cities = _cityRepository.GetCities();

            searchFiltersViewModel.OrderBy = new List<Status>
            {
                new Status { Id = 0, Name ="Default"},new Status{Id=1, Name="Price Ascending"},new Status{Id= 2, Name="Price Descending"}
            };
            //int? rooms, int? adults, int? children, int? destination, int? order
            HttpCookie cookie = Request.Cookies["SearchParam"];
            if (cookie != null)
            {
                if (cookie["rooms"] != "null" && !String.IsNullOrWhiteSpace(cookie["rooms"]))
                {
                    searchFiltersViewModel.Rooms =int.Parse(cookie["rooms"]);
                }
                if (cookie["adults"] != "null" && !String.IsNullOrWhiteSpace(cookie["adults"]))
                {
                    searchFiltersViewModel.Adults = int.Parse(cookie["adults"]);
                }
                if (cookie["children"] != "null" && !String.IsNullOrWhiteSpace(cookie["children"]))
                {
                    searchFiltersViewModel.Children = int.Parse(cookie["children"]);
                }
                if (cookie["destination"] != "null" && !String.IsNullOrWhiteSpace(cookie["rooms"]))
                {
                    ViewBag.destination = int.Parse(cookie["destination"]);
                }
                if (cookie["order"] != "null" && !String.IsNullOrWhiteSpace(cookie["rooms"]))
                {
                    ViewBag.order = int.Parse(cookie["order"]);
                }
            }


            return View(searchFiltersViewModel);
        }
        public void createApartmentReview(int rating, string reviewDetails,int apartmentId)
        {
            //apartment repo
            string currentUserID=User.Identity.GetUserId();
            int currentUserIDInt=int.Parse(currentUserID);
            ApartmentReview apartmentReview = new ApartmentReview
            {
                Guid = Guid.NewGuid(),
                ApartmentId = apartmentId,
                UserId = currentUserIDInt,
                Details = reviewDetails,
                Stars = rating
            };
            if (!String.IsNullOrWhiteSpace(currentUserID))
            {
                _apartmentRepository.CreateApartmentReview(apartmentReview);
            }
            else
            {
                //Throw error

            }
        }
        public void createApartmentReservation(string name, string email, string phone,string details,string address, int apartmentId)
        {
            //apartment repo
            string currentUserID = User.Identity.GetUserId();

            int? currentUserIDInt = NullableParse(currentUserID);

            ApartmentReservation apartmentReservation = new ApartmentReservation
            {
                Guid = Guid.NewGuid(),
                ApartmentId = apartmentId,
                Details = details,
                UserId = currentUserIDInt,
                UserName = name,
                UserEmail = email,
                UserPhone = phone,
                UserAddress = address,
            };

            _apartmentReservationRepository.CreateApartmentReservation(apartmentReservation);
            
        }
        private int? NullableParse(string input)
        {
            int output = 0;

            if (!int.TryParse(input, out output))
            {
                return null;
            }

            return output;
        }

        public JsonResult searchApartments(int? rooms, int? adults, int? children, int? destination, int? order,bool useCookie)
        {
            List<ApartmentSearchViewModel> apartments = new List<ApartmentSearchViewModel>();
            apartments = _apartmentRepository.SearchApartments(rooms, adults, children, destination, order);
            if (useCookie)
            {
            FormToCookie(rooms,adults,children,destination,order);
            }
            return Json(apartments);
        }
        public JsonResult loadApartmentImages(int apartmentId)
        {
            //todo fix this
            List<ApartmentPicture> images = _apartmentRepository.GetApartmentPicturesPublic(apartmentId);
            var jsonResult = Json(images);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        private void FormToCookie(int? rooms, int? adults, int? children, int? destination, int? order)
        {
            HttpCookie cookie = Request.Cookies["SearchParam"];
            if (cookie == null)
                cookie = new HttpCookie("SearchParam");

           // string str = a.HasValue ? a.Value.ToString() : string.Empty;

            cookie["rooms"] =rooms.HasValue?rooms.Value.ToString() : "null";
            cookie["adults"] =adults.HasValue?adults.Value.ToString() : "null";
            cookie["children"] = children.HasValue ? children.Value.ToString() : "null";
            cookie["destination"] = destination.HasValue ? destination.Value.ToString() : "null";
            cookie["order"] = order.HasValue ? order.Value.ToString() : "null";

            cookie.Expires = DateTime.Now.AddMinutes(30);
            Response.SetCookie(cookie);
        }


        // GET: Apartments/Details/5
        public ActionResult Details(int? id)
        {
            /*
            var path = "preuzmi (10).jpg";
            // Popravi putanju do slike, u bazi nije cijela putanja!
            var javnoRoot = Server.MapPath("~");
            var adminRoot = Path.Combine(javnoRoot, "../Admin/Content/Pictures");
            var picturePathFake = Path.Combine(adminRoot, path);

            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            string uplImagesRoot = Path.GetDirectoryName(Path.GetDirectoryName(basedir)) + "\\Admin\\Content\\Pictures\\";
            string picturePath = uplImagesRoot + "preuzmi (10).jpg";

            string mimeType = MimeMapping.GetMimeMapping(picturePath);

            var filesomething= new FilePathResult(picturePath, mimeType);

            */

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string captchaResponse = Request["g-recaptcha-response"];
            if (captchaResponse != null && string.IsNullOrWhiteSpace(captchaResponse))
            {
                //Captha not checked show error in server
                ViewBag.CapthaShowError = "block";
            }
            else
            {
                ViewBag.CapthaShowError = "none";

            }

            ApartmentDetailsViewModel viewModel = new ApartmentDetailsViewModel();
            AspNetUser currentUser;
           // List<ApartmentPicture> apartmentPictures = _apartmentRepository.GetApartmentPicturesPublic(id.Value);
            if (User.Identity.IsAuthenticated)
            {
                currentUser = _userRepository.GetUser(int.Parse(User.Identity.GetUserId()));
                viewModel.User = currentUser;
            }

            Apartment apartment = _apartmentRepository.GetApartment(id.Value);
            apartment.Tags = _apartmentRepository.GetApartmentTags(id.Value);
            apartment.ApartmentReviews = _apartmentRepository.GetApartmentReviews(id.Value);
            apartment.ApartmentPictures=_apartmentRepository.GetApartmentPicturesPublic(id.Value);

            viewModel.Apartment = apartment;
           
            //dbApartment.ApartmentPictures = _apartmentRepository.GetApartmentPictures(id.Value);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }


        [HttpPost]
        public JsonResult AjaxMethod(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + "6LfI9XYhAAAAAAU7-ys6LzivfAEiWae_ojzthjtT" + "&response=" + response;
            string responseStr = (new WebClient()).DownloadString(url);
            return Json(responseStr);
        }

        public ActionResult Picture(string path)
        {
            if (path == null || string.IsNullOrEmpty(path))
                return Content(content: "File missing"); // Rješenje "nabrzaka", nije najbolje

            // Popravi putanju do slike, u bazi nije cijela putanja!

            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            string uplImagesRoot = Path.GetDirectoryName(Path.GetDirectoryName(basedir)) + "\\Admin\\Content\\Pictures\\";
            var picturePath=Path.Combine(uplImagesRoot, path);
            string mimeType = MimeMapping.GetMimeMapping(picturePath);
            FilePathResult test = new FilePathResult(picturePath, mimeType);
            return new FilePathResult(picturePath, mimeType);
        }



    }

}
