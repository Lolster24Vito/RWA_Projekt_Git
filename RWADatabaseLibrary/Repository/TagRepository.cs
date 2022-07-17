using Microsoft.ApplicationBlocks.Data;
using RWADatabaseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWADatabaseLibrary.Repository
{
    public class TagRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["apartments"].ConnectionString;

        public List<Tag> GetTagsDdl()
        {
            var ds = SqlHelper.ExecuteDataset(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.GetTags");
            var tagList = new List< Tag>();
            tagList.Add(new  Tag { Id = 0, Name = "(odabir taga)" });
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var tag = new  Tag();
                tag.Id = Convert.ToInt32(row["ID"]);
                tag.Name = row["Name"].ToString();
                tagList.Add(tag);
            }
            return tagList;
        }
        public List<Tag> GetTags()
        {
            var ds = SqlHelper.ExecuteDataset(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.GetTags");
            var tagList = new List<Tag>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var tag = new Tag();
                tag.Id = Convert.ToInt32(row["ID"]);
                tag.Name = row["Name"].ToString();
                tagList.Add(tag);
            }
            return tagList;
        }
        public int GetTagCount(int tagId)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@tagID", tagId));
            var ds = SqlHelper.ExecuteDataset(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.LoadApartmentsByTagID",
            commandParameters.ToArray());

            return ds.Tables[0].Rows.Count;
            
        }

    }
}
