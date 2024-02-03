using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo;

public partial class Brand
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
