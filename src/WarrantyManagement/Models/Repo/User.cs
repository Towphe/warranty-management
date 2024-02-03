using System;
using System.Collections.Generic;

namespace WarrantyManagement.Models.Repo;

public partial class User
{
    public int Id { get; set; }

    public int AddressId { get; set; }

    public int RoleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string? Password { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Entry> Entries { get; } = new List<Entry>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Status> Statuses { get; } = new List<Status>();
}
