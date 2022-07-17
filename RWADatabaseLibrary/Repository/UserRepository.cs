using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ApplicationBlocks.Data;
using RWADatabaseLibrary.Models;
using System.Configuration;
using System.Data;

namespace RWADatabaseLibrary.Repository
{
    public class UserRepository
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["apartments"].ConnectionString;

        public List<AspNetUser> GetUsers()
        {
            var ds = SqlHelper.ExecuteDataset(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.GetUsers");
            var userList = new List<AspNetUser>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var user = new AspNetUser();
                user.Id = Convert.ToInt32(row["ID"]);
                user.Guid = Guid.Parse(row["Guid"].ToString());
                user.CreatedAt = Convert.ToDateTime(row["CreatedAt"]);
                user.DeletedAt = row["DeletedAt"] != DBNull.Value ?
                (DateTime?)Convert.ToDateTime(row["DeletedAt"]) :
                null;
                user.Email = row["Email"].ToString();
                user.EmailConfirmed = bool.Parse(row["EmailConfirmed"].ToString());
                user.PasswordHash = row["PasswordHash"].ToString();
                user.SecurityStamp = row["SecurityStamp"].ToString();
                user.PhoneNumber = row["PhoneNumber"].ToString();
                user.PhoneNumberConfirmed= bool.Parse(row["PhoneNumberConfirmed"].ToString());
                user.LockoutEndDateUtc = row["LockoutEndDateUtc"] != DBNull.Value ?
                (DateTime?)Convert.ToDateTime(row["LockoutEndDateUtc"]) :
                null;
                user.LockoutEnabled = bool.Parse(row["LockoutEnabled"].ToString());
                user.AccessFailedCount = (int)Convert.ToInt32(row["AccessFailedCount"]);
                user.UserName = row["UserName"].ToString();
                user.Address = row["Address"].ToString();
                 userList.Add(user);
            }
            return userList;
        }
    }
}
