using System;
using System.Collections.Generic;

namespace DATC_Core.Models
{
    public partial class Slider
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Position { get; set; }
        public string? Image { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Status { get; set; }
    }
}
