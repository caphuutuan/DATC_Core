using System;
using System.Collections.Generic;

namespace DATC_Core.Models
{
    public partial class PostCategory
    {
        public PostCategory()
        {
            Posts = new HashSet<Post>();
        }

        public int CateId { get; set; }
        public string CateName { get; set; } = null!;
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public int? Levels { get; set; }
        public int? Ordering { get; set; }
        public bool? Published { get; set; }
        public string? Cover { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
