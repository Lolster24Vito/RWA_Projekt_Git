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

        // GET: Apartments
        public ActionResult Index()
        {
            SearchFiltersViewModel searchFiltersViewModel = new SearchFiltersViewModel();
            searchFiltersViewModel.Cities = _cityRepository.GetCities();
            searchFiltersViewModel.OrderBy = new List<Status>
            {
                new Status { Id = 0, Name ="Default"},new Status{Id=1, Name="Price Ascending"},new Status{Id= 2, Name="Price Descending"}
            };
                
            return View(searchFiltersViewModel);
        }
        public JsonResult searchApartments(int? rooms, int? adults, int? children, int? destination, int? order)
        {
            List<ApartmentSearchViewModel> apartments = new List<ApartmentSearchViewModel>();
            apartments = _apartmentRepository.SearchApartments(rooms, adults, children, destination, order);
            
            return Json(apartments);
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
