using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo;

public partial class Address
{
    public int Id { get; set; }

    public string? Region { get; set; }

    public string? City { get; set; }

    public string? Brgy { get; set; }

    public string? Street { get; set; }

    public int? Zipcode { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
