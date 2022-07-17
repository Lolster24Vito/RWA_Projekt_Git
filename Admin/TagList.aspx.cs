using Admin.Models;
using RWADatabaseLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class TagList : System.Web.UI.Page
    {
        private readonly TagRepository _tagRepository = new TagRepository();
        private List<TagCount> tagList=new List<TagCount>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               var tags = _tagRepository.GetTags();
                foreach (var tag in tags)
                {
                    tagList.Add(new TagCount { Id = tag.Id, Name = tag.Name, Count = _tagRepository.GetTagCount(tag.Id) });
                }
                repTags.DataSource=tagList;
                repTags.DataBind();
            }
        }

        protected void repTags_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem ri = e.Item;
            if (ri.DataItem != null)
            {

            var dataItem = ri.DataItem as TagCount;
            var link = ri.FindControl("hlDelete") as HyperLink;
            link.Visible = dataItem.Count <= 0;
            }


        }

        protected void lbTagAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("TagAdd.aspx");

        }
    }
}