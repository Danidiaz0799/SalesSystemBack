using System;
using System.Collections.Generic;

namespace SalesSystem.Core.Models;

public partial class Shop
{
    public int ShopId { get; set; }

    public string ShopName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int ManagerId { get; set; }

    public virtual ICollection<Advisor> Advisors { get; } = new List<Advisor>();

    public virtual Manager Manager { get; set; } = null!;
}
