using System;
using System.Collections.Generic;

namespace SalesSystem.Core.Models;

public partial class SalesStatus
{
    public int SaleStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();
}
