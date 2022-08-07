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
    public partial class TagAdd : System.Web.UI.Page
    {
        private readonly TagRepository _tagRepository = new TagRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RebindTagType();

            }
        }

        private void RebindTagType()
        {
            ddlTagType.DataSource = _tagRepository.GetTagTypes();
            ddlTagType.DataBind();
        }

        protected void lblSave_Click(object sender, EventArgs e)
        {
            string tagName = tbName.Text;
            string tagEngName = tbNameEng.Text;
            if (string.IsNullOrWhiteSpace(tagName) || string.IsNullOrWhiteSpace(tagEngName))
            {
                ErrorMessage.Visible = true;
                ErrorMessage.InnerText = "Molim vas popunite sve vrijednosti";
                return;
            }
            ErrorMessage.Visible = false;

            _tagRepository.CreateTag(tagName, tagEngName,ddlTagType.SelectedIndex+1);
            Response.Redirect($"TagList.aspx");

        }



        protected void btnTagList_Click(object sender, EventArgs e)
        {

        }
    }
}