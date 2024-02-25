using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo
{
    public partial class Product
    {
        public Product()
        {
            Variations = new HashSet<Variation>();
            WarrantyEntries = new HashSet<WarrantyEntry>();
        }

        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual ICollection<Variation> Variations { get; set; }
        public virtual ICollection<WarrantyEntry> WarrantyEntries { get; set; }
    }
}
