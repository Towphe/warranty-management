using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo;

public partial class Variation
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public virtual ICollection<Entry> Entries { get; } = new List<Entry>();

    public virtual Product Product { get; set; } = null!;
}
