using System;
using System.Collections.Generic;

namespace SalesSystem.Core.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int SalePrice { get; set; }

    public int PurchasePrice { get; set; }

    public string ProductName { get; set; } = null!;

    public int Stock { get; set; }

    public int DealerId { get; set; }

    public virtual Dealer Dealer { get; set; } = null!;
}
