using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RWADatabaseLibrary.Models
{
    public class ApartmentPicture
    {
        public int? Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public bool IsRepresentative { get; set; }
        public bool DoDelete { get; set; }

        public static ApartmentPicture CreateApartmentFromPath(string path)
        {

            return new ApartmentPicture
            {
                Path = path,
                Name =System.IO.Path.GetFileNameWithoutExtension(path),
                IsRepresentative = false,
            };
        }
       
    }

}
