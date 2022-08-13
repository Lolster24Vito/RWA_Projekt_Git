using RWADatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Javno.Models.ViewModels
{
    public class SearchFiltersViewModel
    {
        public List<City> Cities { get; set; }
        public List<Status> OrderBy { get; set; }
    }
}