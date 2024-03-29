﻿using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo
{
    public partial class Variation
    {
        public Variation()
        {
            WarrantyEntries = new HashSet<WarrantyEntry>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<WarrantyEntry> WarrantyEntries { get; set; }
    }
}
