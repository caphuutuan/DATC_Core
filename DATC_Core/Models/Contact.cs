using System;
using System.Collections.Generic;

namespace DATC_Core.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Title { get; set; } = null!;
        public string? Detail { get; set; }
        public string? Reply { get; set; }
        public int Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
