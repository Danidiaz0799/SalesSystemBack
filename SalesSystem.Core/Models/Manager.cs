using System;
using System.Collections.Generic;

namespace SalesSystem.Core.Models;

public partial class Manager
{
    public int ManagerId { get; set; }

    public string FullName { get; set; } = null!;

    public string Identification { get; set; } = null!;

    public int? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();

    public virtual ICollection<Shop> Shops { get; } = new List<Shop>();
}
