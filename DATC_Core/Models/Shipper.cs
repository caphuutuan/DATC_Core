using System;
using System.Collections.Generic;

namespace DATC_Core.Models
{
    public partial class Shipper
    {
        public int ShipperId { get; set; }
        public string ShipperName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Company { get; set; }
        public DateTime? ShipDate { get; set; }
    }
}
