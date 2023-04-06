using System;
using System.Collections.Generic;

namespace SalesSystem.Core.Models;

public partial class Dealer
{
    public int DealerId { get; set; }

    public string DealerName { get; set; } = null!;

    public string Nit { get; set; } = null!;

    public int? Phone { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    public virtual ICollection<Purchase> Purchases { get; } = new List<Purchase>();
}
