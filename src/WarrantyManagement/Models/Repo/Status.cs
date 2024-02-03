using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo;

public partial class Status
{
    public int Id { get; set; }

    public int EntryId { get; set; }

    public int ConsigneeId { get; set; }

    public string? Name { get; set; }

    public bool? IsCurrent { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public virtual User Consignee { get; set; } = null!;

    public virtual Entry Entry { get; set; } = null!;
}
