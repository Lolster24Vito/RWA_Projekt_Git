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

namespace Javno.Controllers
{
    public class ApartmentsController : Controller
    {
        private ApartmentRepository _apartmentRepository = new ApartmentRepository();
        private CityRepository _cityRepository = new CityRepository();

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
                /* if (cookie["City"] != null)
                 {
                     ddlCity.SelectedValue = cookie["City"];
                 }
                 if (cookie["Order"] != null)
                 {
                     ddlOrder.SelectedValue = cookie["Order"];
                 }*/
            }


            return View(searchFiltersViewModel);
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = _apartmentRepository.GetApartment(id.Value);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }


    }
}
