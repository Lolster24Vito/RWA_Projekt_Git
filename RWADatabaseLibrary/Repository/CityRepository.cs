using Microsoft.ApplicationBlocks.Data;
using RWADatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWADatabaseLibrary.Repository
{
    public  class CityRepository
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["apartments"].ConnectionString;
        public List<City> GetCities()
        {
            var ds = SqlHelper.ExecuteDataset(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.GetCities");
            var cityList = new List<City>();
            cityList.Add(new City { Id = 0, Name = "(odabir grada)" });
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var city = new City();
                city.Id = Convert.ToInt32(row["ID"]);
                city.Guid = Guid.Parse(row["Guid"].ToString());
                city.Name = row["Name"].ToString();
                cityList.Add(city);
            }
            return cityList;
        }
    }
}
