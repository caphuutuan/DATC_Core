using System;
using System.Collections.Generic;

namespace DATC_Core.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public int? TransactStatusId { get; set; }
        public bool? Deleted { get; set; }
        public bool? Paid { get; set; }
        public int? PaymentId { get; set; }
        public string? Note { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual TransactStatuss? TransactStatus { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
