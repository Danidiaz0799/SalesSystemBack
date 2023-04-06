using System;
using System.Collections.Generic;

namespace SalesSystem.Core.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public int AdvisorId { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public int SaleStatusId { get; set; }

    public int CustomerId { get; set; }

    public virtual Advisor Advisor { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual SalesStatus SaleStatus { get; set; } = null!;
}
