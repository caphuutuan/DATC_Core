using System;
using System.Collections.Generic;

namespace DATC_Core.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
