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
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserAddress { get; set; }
        public int? Rating { get; set; }

    }
}