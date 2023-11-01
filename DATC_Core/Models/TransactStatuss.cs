using System;
using System.Collections.Generic;

namespace DATC_Core.Models
{
    public partial class TransactStatuss
    {
        public TransactStatuss()
        {
            Orders = new HashSet<Order>();
        }

        public int TransactStatusId { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
