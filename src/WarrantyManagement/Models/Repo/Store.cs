using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo
{
    public partial class Store
    {
        public Store()
        {
            WarrantyEntries = new HashSet<WarrantyEntry>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<WarrantyEntry> WarrantyEntries { get; set; }
    }
}
