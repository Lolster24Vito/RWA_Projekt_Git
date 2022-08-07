using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWADatabaseLibrary.Repository
{
    public class OrderRepository
    {
        public List<Models.Order> GetOrders()
        {
            return new List<Models.Order>
                 {
                 new Models.Order { Id = 0, Name = "(kriterij sortiranja)" },
                 new Models.Order { Id = 1, Name = "Broj Soba (uzlazno)" },
                 new Models.Order { Id = 11, Name = "Broj Soba (silazno)" },
                 new Models.Order { Id = 2, Name = "Broj Odraslih (ulazno)" },
                 new Models.Order { Id = 22, Name = "Broj Odraslih (silazno)" },
                 new Models.Order { Id = 3, Name = "Broj Djece (silazno)" },
                 new Models.Order { Id = 33, Name = "Broj Djece (silazno)" },
                 new Models.Order { Id = 4, Name = "Cijena (ulazno)" },
                 new Models.Order { Id = 44, Name = "Cijena (silazno)" },
                 };
        }


    }
}
