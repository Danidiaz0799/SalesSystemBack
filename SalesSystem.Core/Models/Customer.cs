using System;
using System.Collections.Generic;

namespace SalesSystem.Core.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FullName { get; set; } = null!;

    public string Identification { get; set; } = null!;

    public int Phone { get; set; }

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();
}
