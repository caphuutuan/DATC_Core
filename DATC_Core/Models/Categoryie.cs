﻿using System;
using System.Collections.Generic;

namespace DATC_Core.Models
{
    public partial class Categoryie
    {
        public Categoryie()
        {
            Products = new HashSet<Product>();
        }

        public int CateId { get; set; }
        public string CateName { get; set; } = null!;
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public int? Levels { get; set; }
        public int? Ordering { get; set; }
        public bool? Published { get; set; }
        public string? Thumb { get; set; }
        public string? Title { get; set; }
        public string? Alias { get; set; }
        public string? MetaDesc { get; set; }
        public string? MetaKey { get; set; }
        public string? Cover { get; set; }
        public string? SchemaMarkup { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}