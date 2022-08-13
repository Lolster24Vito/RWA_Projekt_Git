using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWADatabaseLibrary.Models.ViewModels
{
    public class ApartmentSearchViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StarRating { get; set; }
        public string CityName { get; set; }
        public int? BeachDistance { get; set; }
        public int? TotalRooms { get; set; }

        public int? MaxAdults { get; set; }
        public int? MaxChildren { get; set; }
        public decimal Price { get; set; }
        public string RepresentativePicturePath { get; set; }
        public string RepresentativePictureBytes { get; set; }

        public int StatusId { get; set; }
    }
}
