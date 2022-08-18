using Microsoft.ApplicationBlocks.Data;
using RWADatabaseLibrary.Models;
using RWADatabaseLibrary.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWADatabaseLibrary.Repository
{
    public class ApartmentRepository
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["apartments"].ConnectionString;

        public List<ApartmentSearchViewModel> SearchApartments(int? rooms, int? adults, int? children, int? destination, int? order)
        {
            string basedir = AppDomain.CurrentDomain.BaseDirectory;
            string uplImagesRoot = Path.GetDirectoryName(Path.GetDirectoryName(basedir)) + "\\Admin\\";
            // uplImagesFolder = Path.GetFullPath(uplImagesRoot + PICPATH);
            var commandParameters = new List<SqlParameter>();
            if (rooms.HasValue)
            {
                commandParameters.Add(new SqlParameter("@rooms", rooms));
            }
            if (adults.HasValue)
            {
                commandParameters.Add(new SqlParameter("@adults", adults));
            }
            if (children.HasValue)
            {
                commandParameters.Add(new SqlParameter("@children", children));
            }
            if (destination.HasValue)
            {
                commandParameters.Add(new SqlParameter("@destination", destination));
            }
            if (order.HasValue)
            {
                commandParameters.Add(new SqlParameter("@order ", order));
            }
            var ds = SqlHelper.ExecuteDataset(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.SearchApartments",
            commandParameters.ToArray());

            var apList = new List<ApartmentSearchViewModel>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var ap = new ApartmentSearchViewModel();
                ap.Id = Convert.ToInt32(row["ID"]);

                ap.Name = row["Name"].ToString();

                ap.CityName = row["CityName"]?.ToString();

                ap.Price = Convert.ToDecimal(row["Price"]);
                ap.MaxAdults =
                row["MaxAdults"] != DBNull.Value ?
                (int?)Convert.ToInt32(row["MaxAdults"]) :
                null;
                ap.MaxChildren =
                row["MaxChildren"] != DBNull.Value ?
                (int?)Convert.ToInt32(row["MaxChildren"]) :
                null;
                ap.TotalRooms =
                row["TotalRooms"] != DBNull.Value ?
                (int?)Convert.ToInt32(row["TotalRooms"]) :
                null;
                ap.BeachDistance =
                row["BeachDistance"] != DBNull.Value ?
                (int?)Convert.ToInt32(row["BeachDistance"]) :
                null;
                ap.RepresentativePicturePath = row["RepresentativePicturePath"].ToString();
                ap.StarRating = row["StarRating"] != DBNull.Value ?
                (int)Convert.ToInt32(row["StarRating"]) :
                0;

                string fullPath = uplImagesRoot + ap.RepresentativePicturePath;
                //TODO CHECK IF FILE EXISTS


                if (String.IsNullOrWhiteSpace(row["RepresentativePictureBytes"].ToString()))
                {
                    if (File.Exists(fullPath))
                    {

                        byte[] byteData = System.IO.File.ReadAllBytes(fullPath);
                        string imreBase64Data = Convert.ToBase64String(byteData);
                        string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                        SetApartmentPictureBase64(ap.RepresentativePicturePath, imreBase64Data);
                        ap.RepresentativePictureBytes = imgDataURL;
                    }
                }
                else
                {
                    ap.RepresentativePictureBytes = string.Format("data:image/png;base64,{0}", row["RepresentativePictureBytes"].ToString());
                }

                if (String.IsNullOrEmpty(ap.RepresentativePictureBytes))
                {
                    ap.RepresentativePictureBytes = "\'\'";
                }
                // ap.RepresentativePictureBytes=
                apList.Add(ap);
            }
            return apList;

        }

        public List<ApartmentReview> GetApartmentReviews(int id)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@ApartmentId", id));
            var ds = SqlHelper.ExecuteDataset(
             _connectionString,
             CommandType.StoredProcedure,
             "dbo.GetApartmentStarRating",
             commandParameters.ToArray());

            var reviewList = new List<ApartmentReview>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var ap = new ApartmentReview();
                ap.Id = Convert.ToInt32(row["ID"]);
                ap.Guid = Guid.Parse(row["Guid"].ToString());
                ap.CreatedAt = Convert.ToDateTime(row["CreatedAt"]);

                ap.ApartmentId = Convert.ToInt32(row["ApartmentId"]);
                ap.UserId = Convert.ToInt32(row["UserId"]);
                ap.Details = row["Details"]?.ToString();
                ap.Stars = Convert.ToInt32(row["Stars"]);
                ap.Username = row["UserName"].ToString();

                reviewList.Add(ap);
            }
            return reviewList;
        }
        public void CreateApartmentReview(ApartmentReview apReview)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@guid", apReview.Guid));
            commandParameters.Add(new SqlParameter("@ApartmentId", apReview.ApartmentId));
            commandParameters.Add(new SqlParameter("@UserId", apReview.UserId));
            commandParameters.Add(new SqlParameter("@Details", apReview.Details));
            commandParameters.Add(new SqlParameter("@Stars", apReview.Stars));

            SqlHelper.ExecuteNonQuery(
             _connectionString,
             CommandType.StoredProcedure,
             "dbo.CreateApartmentReview",
             commandParameters.ToArray());

        }


        public List<Apartment> GetApartments(int? statusId, int? cityId, int? order)
        {
            var commandParameters = new List<SqlParameter>();
            if (statusId.HasValue && statusId.Value != 0)
                commandParameters.Add(new SqlParameter("@statusId", statusId));
            if (cityId.HasValue && cityId.Value != 0)
                commandParameters.Add(new SqlParameter("@cityId", cityId));
            if (order.HasValue && order.Value != 0)
                commandParameters.Add(new SqlParameter("@order", order));
            var ds = SqlHelper.ExecuteDataset(
             _connectionString,
             CommandType.StoredProcedure,
             "dbo.GetApartments",
             commandParameters.ToArray());

            var apList = new List<Apartment>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var ap = new Apartment();
                ap.Id = Convert.ToInt32(row["ID"]);
                ap.Guid = Guid.Parse(row["Guid"].ToString());
                ap.CreatedAt = Convert.ToDateTime(row["CreatedAt"]);
                ap.DeletedAt =
                row["DeletedAt"] != DBNull.Value ?
                (DateTime?)Convert.ToDateTime(row["DeletedAt"]) :
                null;
                ap.OwnerId = Convert.ToInt32(row["OwnerId"]);
                ap.OwnerName = row["OwnerName"].ToString();
                ap.TypeId = Convert.ToInt32(row["TypeId"]);
                ap.StatusId = Convert.ToInt32(row["StatusId"]);
                ap.StatusName = row["StatusName"].ToString();
                ap.CityId =
                row["CityId"] != DBNull.Value ?
                (int?)Convert.ToInt32(row["CityId"]) :
                null;
                ap.CityName = row["CityName"]?.ToString();
                ap.Address = row["Address"].ToString();
                ap.Name = row["Name"].ToString();
                ap.Price = Convert.ToDecimal(row["Price"]);
                ap.MaxAdults =
                row["MaxAdults"] != DBNull.Value ?
                (int?)Convert.ToInt32(row["MaxAdults"]) :
                null;
                ap.MaxChildren =
                row["MaxChildren"] != DBNull.Value ?
                (int?)Convert.ToInt32(row["MaxChildren"]) :
                null;
                ap.TotalRooms =
                row["TotalRooms"] != DBNull.Value ?
                (int?)Convert.ToInt32(row["TotalRooms"]) :
                null;
                ap.BeachDistance =
                row["BeachDistance"] != DBNull.Value ?
                (int?)Convert.ToInt32(row["BeachDistance"]) :
                null;
                apList.Add(ap);
            }
            return apList;
        }
        public void SetApartmentPictureBase64(string path, string base64)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@path", path));
            commandParameters.Add(new SqlParameter("@base64", base64));

            SqlHelper.ExecuteNonQuery(
                _connectionString,
                CommandType.StoredProcedure,
                "dbo.SetApartmentPictureBase64",
                commandParameters.ToArray());


        }
        public void CreateApartment(Apartment apartment)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@guid", apartment.Guid));
            commandParameters.Add(new SqlParameter("@ownerId", apartment.OwnerId));
            commandParameters.Add(new SqlParameter("@typeId", apartment.TypeId));
            commandParameters.Add(new SqlParameter("@statusId", apartment.StatusId));
            commandParameters.Add(new SqlParameter("@cityId", apartment.CityId));
            commandParameters.Add(new SqlParameter("@address", apartment.Address));
            commandParameters.Add(new SqlParameter("@name", apartment.Name));
            commandParameters.Add(new SqlParameter("@price", apartment.Price));
            commandParameters.Add(new SqlParameter("@maxAdults", apartment.MaxAdults));
            commandParameters.Add(new SqlParameter("@maxChildren", apartment.MaxChildren));
            commandParameters.Add(new SqlParameter("@totalRooms", apartment.TotalRooms));
            commandParameters.Add(new SqlParameter("@beachDistance", apartment.BeachDistance));

            //tag columns
            DataTable dtTags = new DataTable();
            dtTags.Columns.AddRange(new DataColumn[1]
            {
                new DataColumn("Key",typeof(int))
            });
            foreach (var tag in apartment.Tags)
                dtTags.Rows.Add(tag.Id);

            commandParameters.Add(new SqlParameter("@tags", dtTags));

            //picture columns
            DataTable dtPics = new DataTable();
            dtPics.Columns.AddRange(
                      new DataColumn[] {
                      new DataColumn("Id", typeof(int)),
                      new DataColumn("Path", typeof(string)),
                      new DataColumn("Name", typeof(string)),
                      new DataColumn("IsRepresentative", typeof(bool)),
                      new DataColumn("DoDelete", typeof(bool)),
                });

            foreach (var apartmentPicture in apartment.ApartmentPictures)
            {
                if (!apartmentPicture.DoDelete)
                {
                    dtPics.Rows.Add(
                                  apartmentPicture.Id,
                                  apartmentPicture.Path,
                                  apartmentPicture.Name,
                                  apartmentPicture.IsRepresentative,
                                  apartmentPicture.DoDelete);
                }
            }
            commandParameters.Add(new SqlParameter("@pictures", dtPics));

            SqlHelper.ExecuteNonQuery(
                 _connectionString,
                 CommandType.StoredProcedure,
                 "dbo.CreateApartment",
                 commandParameters.ToArray());

        }

        public Apartment GetApartment(int id)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@id", id));
            var ds = SqlHelper.ExecuteDataset(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.GetApartment",
            commandParameters.ToArray());
            var row = ds.Tables[0].Rows[0];
            var ap = new Apartment();
            ap.Id = Convert.ToInt32(row["ID"]);
            ap.Guid = Guid.Parse(row["Guid"].ToString());
            ap.CreatedAt = Convert.ToDateTime(row["CreatedAt"]);
            ap.DeletedAt =
            row["DeletedAt"] != DBNull.Value ?
            (DateTime?)Convert.ToDateTime(row["DeletedAt"]) :
            null;
            ap.OwnerId = Convert.ToInt32(row["OwnerId"]);
            ap.OwnerName = row["OwnerName"].ToString();
            ap.TypeId = Convert.ToInt32(row["TypeId"]);
            ap.StatusId = Convert.ToInt32(row["StatusId"]);
            ap.StatusName = row["StatusName"].ToString();
            ap.CityId =
            row["CityId"] != DBNull.Value ?
            (int?)Convert.ToInt32(row["CityId"]) :
            null;
            ap.CityName = row["CityName"]?.ToString();
            ap.Address = row["Address"].ToString();
            ap.Name = row["Name"].ToString();
            ap.Price = Convert.ToDecimal(row["Price"]);
            ap.MaxAdults =
            row["MaxAdults"] != DBNull.Value ?
            (int?)Convert.ToInt32(row["MaxAdults"]) :
            null;
            ap.MaxChildren =
            row["MaxChildren"] != DBNull.Value ?
            (int?)Convert.ToInt32(row["MaxChildren"]) :
            null;
            ap.TotalRooms =
            row["TotalRooms"] != DBNull.Value ?
            (int?)Convert.ToInt32(row["TotalRooms"]) :
            null;
            ap.BeachDistance =
            row["BeachDistance"] != DBNull.Value ?
            (int?)Convert.ToInt32(row["BeachDistance"]) :
            null;
            return ap;
        }
        public List<Tag> GetApartmentTags(int apartmentId)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@apartmentId", apartmentId));
            var ds = SqlHelper.ExecuteDataset(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.GetApartmentTags",
            commandParameters.ToArray());
            var tags = new List<Tag>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                tags.Add(new Models.Tag
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                });
            }
            return tags;
        }
        public void UpdateApartment(Apartment apartment)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@id", apartment.Id));
            commandParameters.Add(new SqlParameter("@guid", apartment.Guid));
            commandParameters.Add(new SqlParameter("@ownerId", apartment.OwnerId));
            commandParameters.Add(new SqlParameter("@typeId", apartment.TypeId));
            commandParameters.Add(new SqlParameter("@statusId", apartment.StatusId));
            commandParameters.Add(new SqlParameter("@cityId", apartment.CityId));
            commandParameters.Add(new SqlParameter("@address", apartment.Address));
            commandParameters.Add(new SqlParameter("@name", apartment.Name));
            commandParameters.Add(new SqlParameter("@price", apartment.Price));
            commandParameters.Add(new SqlParameter("@maxAdults", apartment.MaxAdults));
            commandParameters.Add(new SqlParameter("@maxChildren", apartment.MaxChildren));
            commandParameters.Add(new SqlParameter("@totalRooms", apartment.TotalRooms));
            commandParameters.Add(new SqlParameter("@beachDistance", apartment.BeachDistance));

            //tags table
            DataTable dtTags = new DataTable();
            dtTags.Columns.AddRange(
                new DataColumn[1] {
              new DataColumn("Key", typeof(int)) });

            foreach (var tag in apartment.Tags)
                dtTags.Rows.Add(tag.Id);

            commandParameters.Add(new SqlParameter("@tags", dtTags));
            //pics table
            DataTable dtPics = new DataTable();
            dtPics.Columns.AddRange(
                new DataColumn[] {
            new DataColumn("Id", typeof(int)),
            new DataColumn("Path", typeof(string)),
            new DataColumn("Name", typeof(string)),
            new DataColumn("IsRepresentative", typeof(bool)),
            new DataColumn("DoDelete", typeof(bool)),
                });

            foreach (var apartmentPicture in apartment.ApartmentPictures)
            {
                if (!apartmentPicture.DoDelete)
                {
                    dtPics.Rows.Add(
                        apartmentPicture.Id,
                        apartmentPicture.Path,
                        apartmentPicture.Name,
                        apartmentPicture.IsRepresentative,
                        apartmentPicture.DoDelete);
                }
            }

            commandParameters.Add(new SqlParameter("@pictures", dtPics));


            SqlHelper.ExecuteNonQuery(
                _connectionString,
                CommandType.StoredProcedure,
                "dbo.UpdateApartment",
                commandParameters.ToArray());

        }
        public List<ApartmentPicture> GetApartmentPictures(int apartmentId)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@apartmentId", apartmentId));

            var ds = SqlHelper.ExecuteDataset(
                _connectionString,
                CommandType.StoredProcedure,
                "dbo.GetApartmentPictures",
                commandParameters.ToArray());


            var pics = new List<ApartmentPicture>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                pics.Add(new Models.ApartmentPicture
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Path = row["Path"].ToString(),
                    Name = row["Name"].ToString(),
                    IsRepresentative = bool.Parse(row["IsRepresentative"].ToString())
                });
            }


            return pics;
        }
        public List<ApartmentPicture> GetApartmentPicturesPublic(int apartmentId)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@apartmentId", apartmentId));

            var ds = SqlHelper.ExecuteDataset(
                _connectionString,
                CommandType.StoredProcedure,
                "dbo.GetApartmentPicturesPublic",
                commandParameters.ToArray());


            var pics = new List<ApartmentPicture>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ApartmentPicture picture = new Models.ApartmentPicture
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Path = row["Path"].ToString(),
                    Name = row["Name"].ToString(),
                    IsRepresentative = bool.Parse(row["IsRepresentative"].ToString()),

                };
                if (String.IsNullOrWhiteSpace(row["Base64Content"].ToString()))
                {
                    string basedir = AppDomain.CurrentDomain.BaseDirectory;
                    string uplImagesRoot = Path.GetDirectoryName(Path.GetDirectoryName(basedir)) + "\\Admin\\";
                    string fullPath = uplImagesRoot + picture.Path;
                    if (File.Exists(fullPath))
                    {
                        byte[] byteData = System.IO.File.ReadAllBytes(fullPath);
                        string imreBase64Data = Convert.ToBase64String(byteData);
                        string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                        SetApartmentPictureBase64(picture.Path, imreBase64Data);
                        picture.Base64Content = imgDataURL;
                        pics.Add(picture);
                    }
                }
                else
                {
                  picture.Base64Content = string.Format("data:image/png;base64,{0}", row["Base64Content"].ToString());
                  pics.Add(picture);
                }
              /*  if (String.IsNullOrEmpty(picture.Base64Content))
                {
                    picture.Base64Content = "\'\'";
                }
                pics.Add(picture);*/
            }


            return pics;
        }


        public void DeleteApartment(int id)
        {
            var commandParameters = new List<SqlParameter>();
            commandParameters.Add(new SqlParameter("@id", id));
            SqlHelper.ExecuteNonQuery(
            _connectionString,
            CommandType.StoredProcedure,
            "dbo.DeleteApartment",
            commandParameters.ToArray());
        }



    }
}
