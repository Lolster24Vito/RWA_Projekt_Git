﻿using Microsoft.ApplicationBlocks.Data;
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
    public class ApartmentOwnerRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["apartments"].ConnectionString;
        public ApartmentOwnerRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["apartments"].ConnectionString;
        }
        public List<ApartmentOwner> GetApartmentOwners()
        {
            var ds = SqlHelper.ExecuteDataset(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.GetApartmentOwners");
            var ownerList = new List<ApartmentOwner>();
            ownerList.Add(new ApartmentOwner { Id = 0, Name = "(odabir vlasnika)" });
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var owner = new ApartmentOwner();
                owner.Id = Convert.ToInt32(row["ID"]);
                owner.Name = row["Name"].ToString();
                ownerList.Add(owner);
            }
            return ownerList;

        }
    }
}
