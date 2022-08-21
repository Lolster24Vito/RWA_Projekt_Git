using RWADatabaseLibrary.Models;
using RWADatabaseLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class ApartmentReservationAdd : System.Web.UI.Page
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly ApartmentRepository _apartmentRepository = new ApartmentRepository();
        private readonly ApartmentReservationRepository _apartmentReservationRepository=new ApartmentReservationRepository();

        private Apartment apartment;
                int? id = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string qryStrId = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(qryStrId))
                {
                    id = int.Parse(qryStrId);
                    //get reservations
                    apartment = _apartmentRepository.GetApartment(id.Value);
                    if (apartment == null)
                    {
                        id = null;
                    Response.Redirect("ApartmentList.aspx");
                    }
                        if (apartment.StatusId == 3) Response.Redirect("ApartmentList.aspx");
                    lApartmentName.InnerText = apartment.Name;
                    RebindUsers();
                }
                else
                {
                    Response.Redirect("ApartmentList.aspx");

                }
            }
            else
            {
                string qryStrId = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(qryStrId))
                {
                    id = int.Parse(qryStrId);
                    //get reservations
                    apartment = _apartmentRepository.GetApartment(id.Value);
                    
                }
            }

           
        }

        private void RebindUsers()
        {
            lbUsers.DataSource = _userRepository.GetUsers();
            lbUsers.DataValueField = "Id";
            lbUsers.DataTextField = "UserName";
            lbUsers.DataBind();
        }

        protected void lblSave_Click(object sender, EventArgs e)
        {
            //http://localhost:60815/ApartmentReservationAdd.aspx?Id=2

            if (checkBoxUseRegistered.Checked&& String.IsNullOrWhiteSpace(lbUsers.SelectedValue)){
                ErrorMessage.InnerText = "Molim vas odaberite korisnika za za rezervaciju";
                ErrorMessage.Visible = true;
                return;
            }
            if(checkBoxUseRegistered.Checked && string.IsNullOrWhiteSpace(tbDetailsRegisteredUser.Text))
                {
                    ErrorMessage.InnerText = "Navedite detalje rezervacije";
                ErrorMessage.Visible = true;

                return;
                }
            ApartmentReservation apartmentReservation = GetReservationForm() ;
            if (!apartmentReservation.UserId.HasValue&&
               (String.IsNullOrWhiteSpace(apartmentReservation.Details)||
                String.IsNullOrWhiteSpace(apartmentReservation.UserName)||
                String.IsNullOrWhiteSpace(apartmentReservation.UserEmail)||
                String.IsNullOrWhiteSpace(apartmentReservation.UserPhone) ||
                String.IsNullOrWhiteSpace(apartmentReservation.UserAddress)
                )
                )
            {
                ErrorMessage.InnerText = "Unesite sve vrijednosti za rezervaciju";
                ErrorMessage.Visible = true;
                return;
            }
            else
            {
                ErrorMessage.Visible = false;

            }
            
            _apartmentReservationRepository.CreateApartmentReservation(apartmentReservation);
            Response.Redirect("ApartmentList.aspx");

        }
        
        private ApartmentReservation GetReservationForm()
        {
            if (checkBoxUseRegistered.Checked)
            {
                return new ApartmentReservation
                {
                    Guid = Guid.NewGuid(),
                    ApartmentId = id.Value,
                    Details = tbDetailsRegisteredUser.Text,
                    UserId = int.Parse(lbUsers.SelectedValue),
                    UserName = null,
                    UserEmail=null,
                    UserPhone=null,
                    UserAddress=null
            };
            }
            else
            {
                return new ApartmentReservation
                {
                    Guid = Guid.NewGuid(),
                    ApartmentId = apartment.Id,
                    Details = tbDetailsNotRegisteredUser.Text,
                    UserId = null,
                    UserName = tbUserName.Text,
                    UserEmail = tbUserEmail.Text,
                    UserPhone = tbUserPhone.Text,
                    UserAddress = tbUserAdress.Text
                };
            }
        }

        protected void btnApartmentList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApartmentList.aspx");

        }
    }



}