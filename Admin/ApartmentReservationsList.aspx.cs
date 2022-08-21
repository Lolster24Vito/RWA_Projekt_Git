using Admin.Models;
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
    public partial class ApartmentReservationsList : System.Web.UI.Page
    {
       private readonly ApartmentReservationRepository _apartmentReservationRepository=new ApartmentReservationRepository();
       private readonly UserRepository _userRepository=new UserRepository();
        private readonly ApartmentRepository _apartmentRepository = new ApartmentRepository();

        private Apartment apartment;
        int? id = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string qryStrId = Request.QueryString["id"];

              
                int id=int.Parse(qryStrId);
                apartment = _apartmentRepository.GetApartment(id);
                if(apartment == null)
                {
                    throw new Exception("Error id does not exist");
                }
                lApartmentName.InnerText = apartment.Name;





                List<ApartmentReservation> apartmentReservations = _apartmentReservationRepository.GetApartmentReservation(id);
                List< ApartmentReservationViewModel> arViewList=new List< ApartmentReservationViewModel>();


                foreach (var reservation in apartmentReservations)
                {
                    ApartmentReservationViewModel arView=new ApartmentReservationViewModel();
                    arView.CreatedAt=reservation.CreatedAt;
                    arView.Details=reservation.Details;
                    if (!reservation.UserId.HasValue)
                    {
                        arView.UserName=reservation.UserName;
                        arView.UserEmail=reservation.UserEmail;
                        arView.UserPhone=reservation.UserPhone;
                        arView.UserAddress=reservation.UserAddress;
                        arView.IsRegistered=false;
                        
                    }
                    else
                    {

                        var aspNetUser=_userRepository.GetUser(reservation.UserId.Value);
                        arView.UserName=aspNetUser.UserName;
                        arView.UserEmail = aspNetUser.Email;
                        arView.UserPhone = aspNetUser.PhoneNumber;
                        arView.UserAddress=aspNetUser.Address;
                        arView.IsRegistered = true;


                    }
                    arViewList.Add(arView);

                }

                gvApartmentReservationsList.DataSource = arViewList;
                gvApartmentReservationsList.DataBind();

            }

        }
    }
}