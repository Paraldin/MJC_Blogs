using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MJC_Blogs.Models
{
    public class Blogs
    {
        public Blogs()
        {
            this.Comments = new HashSet<Comments>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public string MediaURL { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public bool Published { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
    }
}