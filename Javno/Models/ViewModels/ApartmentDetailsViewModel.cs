using RWADatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Javno.Models.ViewModels
{
    public class ApartmentDetailsViewModel
    {
        public Apartment Apartment { get; set; }
        public AspNetUser User { get; set; }

    }
}