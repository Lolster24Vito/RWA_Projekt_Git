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
    public partial class TagDelete : System.Web.UI.Page
    {
        TagRepository _tagRepository = new TagRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            string qryStrId = Request.QueryString["id"];
            int? id = null;
            if (!string.IsNullOrEmpty(qryStrId))
            {
                id = int.Parse(qryStrId);
                var dbTag = _tagRepository.GetTag(id.Value);
                SetFormTag(dbTag);
            }
        }

        private void SetFormTag(Tag tag)
        {
            lblTagName.Text = tag.Name;
        }

        protected void lbConfirmDelete_Click(object sender, EventArgs e)
        {
            string qryStrId = Request.QueryString["id"];
            var id = int.Parse(qryStrId);
            _tagRepository.DeleteTag(id);
            Response.Redirect("TagList.aspx");
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("TagList.aspx");

        }
    }
}