using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Javno.Models.ViewModels
{
    public class ContactReservationViewModel
    {
        public int ApartmentId { get; set; }
        public int Details { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
    }
}