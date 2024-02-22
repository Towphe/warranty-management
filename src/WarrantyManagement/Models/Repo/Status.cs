using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo
{
    public partial class Status
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
