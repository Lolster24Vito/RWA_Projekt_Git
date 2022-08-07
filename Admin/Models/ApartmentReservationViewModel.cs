using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class ApartmentReservationViewModel
    {
		
		public DateTime CreatedAt { get; set; }
		public string Details { get; set; }
		public string UserName { get; set; }
		public string UserEmail { get; set; }
		public string UserPhone { get; set; }
		public string UserAddress { get; set; }
        public bool IsRegistered { get; set; }
    }
}