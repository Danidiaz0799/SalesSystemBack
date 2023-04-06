using System;
using System.Collections.Generic;

namespace SalesSystem.Core.Models;

public partial class PurchaseStatus
{
    public int PurchaseStatusId { get; set; }

    public string PurchaseName { get; set; } = null!;

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}
