using Admin.Models;
using RWADatabaseLibrary.Models;
using RWADatabaseLibrary.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class ApartmentEditor : System.Web.UI.Page
    {
        private readonly StatusRepository _statusRepository = new StatusRepository();
        private readonly CityRepository _cityRepository = new CityRepository();
        private readonly ApartmentOwnerRepository _apartmentOwnerRepository = new ApartmentOwnerRepository();
        private readonly TagRepository _tagRepository = new TagRepository();
        private readonly ApartmentRepository _apartmentRepository = new ApartmentRepository();

        public List<Tag> Tags { get; set; }
        public static List<ApartmentPicture> ApartmentPictures { get; set; }

        private const string PICPATH = "/Content/Pictures/";

        private bool IsApartmentExisting;
        protected void Page_Load(object sender, EventArgs e)
        {
            //string uplImagesRoot = Server.MapPath(PICPATH);
            string uplImagesRoot= Path.GetDirectoryName(Path.GetDirectoryName(Server.MapPath("~")));
            string uplImagesFolder = Path.GetFullPath(uplImagesRoot + PICPATH);
      //      string root = Server.MapPath("~");
       //    string parent = Path.GetDirectoryName(root);
       //     string grandParent = Path.GetDirectoryName(parent);
            //  string testString =
           //  string uplImagePath = Path.Combine(uplImagesRoot, uploadedFile.FileName);

            if (!IsPostBack)
            {
                string qryStrId = Request.QueryString["id"];

                int? id = null;
                if (!string.IsNullOrEmpty(qryStrId))
                {
                    id = int.Parse(qryStrId);

                }



                if (id.HasValue)
                {

                    var dbApartment = _apartmentRepository.GetApartment(id.Value);
                    dbApartment.Tags = _apartmentRepository.GetApartmentTags(id.Value);
                    dbApartment.ApartmentPictures = _apartmentRepository.GetApartmentPictures(id.Value);
                    for (int i = 0; i < dbApartment.ApartmentPictures.Count; i++)
                    {
                        dbApartment.ApartmentPictures[i].Path = Path.Combine(PICPATH, dbApartment.ApartmentPictures[i].Path);
                    }
                    IsApartmentExisting = true;
                    SetExistingApartment(dbApartment);
                }
                else
                {
                    IsApartmentExisting=false;
                }




                ddlTags.SelectedIndexChanged += ddlTags_SelectedIndexChanged;
                RebindApartmentOwners();
                RebindCities();
                RebindStatuses();
                RebindTags();

            }
            if (IsPostBack)
            {
               
            }
        }



        private void SetExistingApartment(Apartment apartment)
        {
            ddlStatus.SelectedValue = apartment.StatusId.ToString();
            ddlApartmentOwner.SelectedValue = apartment.OwnerId.ToString();
            tbName.Text = apartment.Name;
            tbAddress.Text = apartment.Address;
            ddlCity.SelectedValue = apartment.CityId.ToString();
            tbPrice.Text = apartment.Price.ToString();
            tbMaxAdults.Text = apartment.MaxAdults?.ToString();
            tbMaxChildren.Text = apartment.MaxChildren?.ToString();
            tbTotalRooms.Text = apartment.TotalRooms?.ToString();
            tbBeachDistance.Text = apartment.BeachDistance?.ToString();

            repTags.DataSource = apartment.Tags;
            repTags.DataBind();

            repApartmentPictures.DataSource = apartment.ApartmentPictures;
            repApartmentPictures.DataBind();
        }

        private void RebindApartmentOwners()
        {
            ddlApartmentOwner.DataSource = _apartmentOwnerRepository.GetApartmentOwners();
            ddlApartmentOwner.DataBind();
        }
        private void RebindCities()
        {
            ddlCity.DataSource = _cityRepository.GetCities();
            ddlCity.DataBind();
        }
        private void RebindStatuses()
        {
            ddlStatus.DataSource = _statusRepository.GetStatuses();
            ddlStatus.DataBind();
        }
        private void RebindTags()
        {
            ddlTags.DataSource = _tagRepository.GetTagsDdl();
            ddlTags.DataBind();
        }

        protected void ddlTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tags = GetRepeaterTags(); 
            var newTag = GetSelectedTag(); 
            if (tags.Any(x => x.Id == newTag.Id))
                return;
            tags.Add(newTag);
            repTags.DataSource = tags;
            repTags.DataBind();

        }
        private List<Tag> GetRepeaterTags()
        {
            var repTagsItems = repTags.Items;
            var tags = new List<Tag>();
            foreach (RepeaterItem item in repTagsItems)
            {
                var tag = new Tag();
                tag.Id = int.Parse((item.FindControl("hidTagId") as HiddenField).Value);
                tag.Name = (item.FindControl("txtTagName") as Label).Text;
                tags.Add(tag);
            }
            return tags;
        }

        private Tag GetSelectedTag()
        {
            var selectedValue = ddlTags.SelectedItem.Value;
            var newTag = new Tag
            {
                Id = int.Parse(ddlTags.SelectedItem.Value),
                Name = ddlTags.SelectedItem.Text
            };
            return newTag;
        }
        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            var tags = GetRepeaterTags();
            var lbSender = (LinkButton)sender;
            var parentItem = (RepeaterItem)lbSender.Parent;
            var hidTagId = (HiddenField)parentItem.FindControl("hidTagId");
            var tagId = int.Parse(hidTagId.Value);
            var existingTag = tags.FirstOrDefault(x => x.Id == tagId);
            tags.Remove(existingTag);
            repTags.DataSource = tags;
            repTags.DataBind();
        }
        protected void lblSave_Click(object sender, EventArgs e)
        {
            var files = SaveUploadedImagesToDisk();
            var apartmentPictures=files.Select(x=>ApartmentPicture.CreateApartmentFromPath(x)).ToList();
            bool isNewApartment = (Request.QueryString["id"] == null);

            Apartment apartment = new Apartment() ;
            try
            {
                apartment = GetFormApartment();
               // if(apartment.this and that isnt in throw error) TODO  VITO

                ErrorMessage.Visible=false;

            }
            catch (Exception)
            {
                ErrorMessage.InnerText = "Molim vas popunite sve vrijednosti";
                ErrorMessage.Visible=true;
                return;

            }

            if (isNewApartment)
            {
                apartment.ApartmentPictures = apartmentPictures;
                _apartmentRepository.CreateApartment(apartment);
                Response.Redirect($"ApartmentList.aspx");
            }
            else
            {
                apartment.Id = int.Parse(Request.QueryString["id"]);
                apartment.ApartmentPictures.AddRange(apartmentPictures);

                _apartmentRepository.UpdateApartment(apartment);
                Response.Redirect($"ApartmentEditor.aspx?{Request.QueryString}");

            }
            Response.Redirect("ApartmentList.aspx");
        }
        private Apartment GetFormApartment()
        {
            int statusId = int.Parse(ddlStatus.SelectedValue);

            int? cityId = int.Parse(ddlCity.SelectedValue);
            if (cityId == 0)
                cityId = null;

            int ownerId = int.Parse(ddlApartmentOwner.SelectedValue);

            decimal price = decimal.Parse(tbPrice.Text);

            int? maxAdults = null;
            if (!string.IsNullOrEmpty(tbMaxAdults.Text))
                maxAdults = int.Parse(tbMaxAdults.Text);

            int? maxChildren = null;
            if (!string.IsNullOrEmpty(tbMaxChildren.Text))
                maxChildren = int.Parse(tbMaxChildren.Text);

            int? totalRooms = null;
            if (!string.IsNullOrEmpty(tbTotalRooms.Text))
                totalRooms = int.Parse(tbTotalRooms.Text);

            int? beachDistance = null;
            if (!string.IsNullOrEmpty(tbBeachDistance.Text))
                beachDistance = int.Parse(tbBeachDistance.Text);

            
            return
          new Apartment
          {
              Guid = Guid.NewGuid(),

              OwnerId = ownerId,
              TypeId = 999, // hardkodirano
              StatusId = statusId,
              CityId = cityId,
              Address = tbAddress.Text,
              Name = tbName.Text,
              Price = price,
              MaxAdults = maxAdults,
              MaxChildren = maxChildren,
              TotalRooms = totalRooms,
              BeachDistance = beachDistance,
              Tags = GetRepeaterTags(),
              ApartmentPictures = GetRepeaterPictures()

          };
        }

        protected void btnApartmentList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApartmentList.aspx");

        }

        private List<string> SaveUploadedImagesToDisk()
        {



            var files = new List<string>();

            if (uplImages.HasFiles)
            {
                //NEW TODO
                /*string uplImagesRootParent = Path.GetDirectoryName(Path.GetDirectoryName(Server.MapPath("~")));
                string uplImagesRoot = Path.GetFullPath(uplImagesRootParent + PICPATH);*/
                 var uplImagesRoot = Server.MapPath(PICPATH); //original
                if (!Directory.Exists(uplImagesRoot))
                    Directory.CreateDirectory(uplImagesRoot);
                foreach (var uploadedFile in uplImages.PostedFiles)
                {
                    string uplImagePath = Path.Combine(uplImagesRoot, uploadedFile.FileName);
                    uploadedFile.SaveAs(uplImagePath);
                    //byte[] byteData = System.IO.File.ReadAllBytes(uplImagePath);
                   // string imreBase64Data = Convert.ToBase64String(byteData);
                    files.Add(uploadedFile.FileName);
                }
            }
            return files;
        }

        protected void repApartmentPictures_DataBinding(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DataBinded");
        }
        public  void DataBindRepeater()
        {
            repApartmentPictures.DataSource = ApartmentPictures;
            repApartmentPictures.DataBind();

        }

        private List<ApartmentPicture> GetRepeaterPictures()
        {
            var repApartmentPicturesItems = repApartmentPictures.Items;
            var pictures = new List<ApartmentPicture>();
            foreach (RepeaterItem item in repApartmentPicturesItems)
            {
                var pic = new ApartmentPicture();
                pic.Id = int.Parse((item.FindControl("hidApartmentPictureId") as HiddenField).Value);
                pic.Name = (item.FindControl("txtApartmentPicture") as TextBox).Text;
                pic.Path = (item.FindControl("imgApartmentPicture") as Image).ImageUrl;
                pic.IsRepresentative = (item.FindControl("cbIsRepresentative") as CheckBox).Checked;
                pic.DoDelete = (item.FindControl("cbDelete") as CheckBox).Checked;
                pictures.Add(pic);
            }
            return pictures;
        }
        [WebMethod]
        public static List<ApartmentPicture> GetPictures()
        {
            return ApartmentPictures;
        }



    }
}