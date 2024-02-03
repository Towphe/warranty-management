using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo;

public partial class Product
{
    public int Id { get; set; }

    public int BrandId { get; set; }

    public int SupplierId { get; set; }

    public string? Sku { get; set; }

    public string? Name { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Entry> Entries { get; } = new List<Entry>();

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual ICollection<Variation> Variations { get; } = new List<Variation>();
}
