using Javno.Models;
using RWADatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Javno.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
           : base("AppDbContext")
        {
        }
        public DbSet<Apartment> Apartments { get; set; }

    }
}