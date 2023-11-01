using System;
using System.Collections.Generic;

namespace DATC_Core.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ShortDesc { get; set; }
        public string? Description { get; set; }
        public int? CateId { get; set; }
        public int? Price { get; set; }
        public int? Discount { get; set; }
        public string? Thumb { get; set; }
        public string? Video { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? BestSeller { get; set; }
        public bool? IsHome { get; set; }
        public bool Active { get; set; }
        public string? Title { get; set; }
        public string? Tags { get; set; }
        public string? Alias { get; set; }
        public string? MetaDesc { get; set; }
        public string? MetaKey { get; set; }
        public int? UnitsInStock { get; set; }

        public virtual Categoryie? Cate { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
