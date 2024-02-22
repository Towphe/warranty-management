using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo
{
    public partial class WarrantyStatus
    {
        public int EntryId { get; set; }
        public int StatusId { get; set; }
        public string Detail { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual WarrantyEntry Entry { get; set; } = null!;
    }
}
