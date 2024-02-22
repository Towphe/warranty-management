using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo
{
    public partial class WarrantyEntry
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int BrandId { get; set; }
        public int ProductId { get; set; }
        public int? VariationId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public short CountryCode { get; set; }
        public string Region { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Baranggay { get; set; } = null!;
        public string Street { get; set; } = null!;
        public short? Zipcode { get; set; }
        public string Detail { get; set; } = null!;
        public string? MediaDir { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual Variation? Variation { get; set; }
    }
}
