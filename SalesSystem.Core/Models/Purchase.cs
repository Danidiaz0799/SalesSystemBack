using System;
using System.Collections.Generic;

namespace SalesSystem.Core.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public DateTime Date { get; set; }

    public int ManagerId { get; set; }

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public int DealerId { get; set; }

    public int PurchaseStatusId { get; set; }

    public virtual Dealer Dealer { get; set; } = null!;

    public virtual Manager Manager { get; set; } = null!;

    public virtual PurchaseStatus PurchaseStatus { get; set; } = null!;
}
