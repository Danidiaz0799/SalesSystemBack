using System;
using System.Collections.Generic;

namespace SalesSystem.Core.Models;

public partial class Advisor
{
    public int AdvisorId { get; set; }

    public string FullName { get; set; } = null!;

    public string Identification { get; set; } = null!;

    public int ShopId { get; set; }

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();

    public virtual Shop Shop { get; set; } = null!;
}
