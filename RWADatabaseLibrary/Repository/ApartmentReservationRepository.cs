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
    public class ApartmentReservationRepository
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["apartments"].ConnectionString;

        public void CreateApartmentReservation(ApartmentReservation reservation)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@guid", reservation.Guid));
            commandParameters.Add(new SqlParameter("@apartmentId", reservation.ApartmentId));
            commandParameters.Add(new SqlParameter("@userid", reservation.UserId));
            commandParameters.Add(new SqlParameter("@username", String.IsNullOrWhiteSpace(reservation.UserName)&&!reservation.UserId.HasValue ?
                null : reservation.UserName));
            commandParameters.Add(new SqlParameter("@userEmail", String.IsNullOrWhiteSpace(reservation.UserEmail) && !reservation.UserId.HasValue ?
                null : reservation.UserEmail));
            commandParameters.Add(new SqlParameter("@userPhone", String.IsNullOrWhiteSpace(reservation.UserPhone) && !reservation.UserId.HasValue ?
                null : reservation.UserPhone));
            commandParameters.Add(new SqlParameter("@userAddress", String.IsNullOrWhiteSpace(reservation.UserAddress) && !reservation.UserId.HasValue ? 
                null : reservation.UserAddress));
            commandParameters.Add(new SqlParameter("@details", reservation.Details));
            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "dbo.CreateApartmentReservation", commandParameters.ToArray());
        }

        public List<ApartmentReservation> GetApartmentReservation(int id)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@apartmentId", id));
            var ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure,
                "dbo.GetApartmentReservation",
                commandParameters.ToArray());
            List<ApartmentReservation> apartmentReservations = new List<ApartmentReservation>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ApartmentReservation ar = new ApartmentReservation();
                ar.Id=Convert.ToInt32(row["ID"]);
                ar.Guid= Guid.Parse(row["Guid"].ToString());
                ar.CreatedAt = Convert.ToDateTime(row["CreatedAt"]);
                ar.ApartmentId= Convert.ToInt32(row["ApartmentId"]);
                ar.Details = row["Details"].ToString();
                ar.UserId= row["UserId"] != DBNull.Value ?
                (int?)Convert.ToInt32(row["UserId"]) :
                null;
                ar.UserName=row["UserName"] != DBNull.Value ? row["UserName"].ToString():String.Empty;
                ar.UserEmail = row["UserEmail"] != DBNull.Value ? row["UserEmail"].ToString() : String.Empty;
                ar.UserPhone = row["UserPhone"] != DBNull.Value ? row["UserPhone"].ToString() : String.Empty;
                ar.UserAddress = row["UserAddress"] != DBNull.Value ? row["UserAddress"].ToString() : String.Empty;
                apartmentReservations.Add(ar);
            }
            return apartmentReservations;
        }

    }
}
