using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo;

public partial class Entry
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int CustomerId { get; set; }

    public int? VariationId { get; set; }

    public string? Serialno { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public virtual User Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Status> Statuses { get; } = new List<Status>();

    public virtual Variation? Variation { get; set; }
}
