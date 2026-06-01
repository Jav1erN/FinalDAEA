using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class BillingDetail
{
    public Guid BillingDetailId { get; set; }

    public Guid BillingId { get; set; }

    public string Description { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? Amount { get; set; }

    public virtual Billing Billing { get; set; } = null!;
}
