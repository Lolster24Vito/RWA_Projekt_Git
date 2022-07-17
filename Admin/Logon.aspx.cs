using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class Logon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // FormsAuthentication.RedirectFromLoginPage() automatically generates
            // the forms authentication cookie!
            if (ValidateUser(txtUserName.Text, txtUserPass.Text))
                FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, chkPersistCookie.Checked);
            else
                Response.Redirect("Logon.aspx", true);
        }

        private bool ValidateUser(string username, string password)
        {
            if(username == "123" || password == "123")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}