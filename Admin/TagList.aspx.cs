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
               var tags = _tagRepository.GetTagsDdl();
                foreach (var tag in tags)
                {
                    tagList.Add(new TagCount { Id = tag.Id,Name=tag.Name, Count = _tagRepository.GetTagCount(tag.Id) });
                }
                repTags.DataSource=tagList;
                repTags.DataBind();
            }
        }
    }
}