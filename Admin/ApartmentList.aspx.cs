using Microsoft.ApplicationBlocks.Data;
using RWADatabaseLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class ApartmentList : System.Web.UI.Page
    {

        private readonly ApartmentRepository _apartmentRepository=new ApartmentRepository();
        private readonly StatusRepository _statusRepository=new StatusRepository();
        private readonly CityRepository _cityRepository=new CityRepository();
        private readonly OrderRepository _orderRepository=new OrderRepository();


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            ddlStatus.DataSource = _statusRepository.GetStatuses();
            ddlStatus.DataBind();
            ddlCity.DataSource = _cityRepository.GetCities();
            ddlCity.DataBind();
            ddlOrder.DataSource = _orderRepository.GetOrders();
            ddlOrder.DataBind();

                CookieToForm();


                RebindApartments();

            }
        }

        public void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebindApartments();
            FormToCookie();


        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebindApartments();
            FormToCookie();


        }

        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebindApartments();
            FormToCookie(); 


        }

        private void RebindApartments()
        {
            var status = int.Parse(ddlStatus.SelectedValue);
            var cityId = int.Parse(ddlCity.SelectedValue);
            var order = int.Parse(ddlOrder.SelectedValue);
            gvApartmentsList.DataSource =
            _apartmentRepository.GetApartments(status, cityId, order);
            gvApartmentsList.DataBind();
        }
        private void FormToCookie()
        {
            HttpCookie cookie = Request.Cookies["ApartmentList"];
            if (cookie == null)
                cookie = new HttpCookie("ApartmentList");

            cookie["Status"] = ddlStatus.SelectedValue;
            cookie["City"] = ddlCity.SelectedValue;
            cookie["Order"] = ddlOrder.SelectedValue;
            cookie.Expires = DateTime.Now.AddMinutes(30);
            Response.SetCookie(cookie);
        }
        private void CookieToForm()
        {
            HttpCookie cookie = Request.Cookies["ApartmentList"];
            if (cookie != null)
            {
                if (cookie["Status"] != null)
                {
                    ddlStatus.SelectedValue = cookie["Status"];
                }
                if (cookie["City"] != null)
                {
                    ddlCity.SelectedValue = cookie["City"];
                }
                if (cookie["Order"] != null)
                {
                    ddlOrder.SelectedValue = cookie["Order"];
                }
            }

        }
        protected void lbApartmentEditor_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApartmentEditor.aspx");
        }

    }
}