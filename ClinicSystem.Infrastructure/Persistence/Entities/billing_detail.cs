using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class billing_detail
{
    public Guid billing_detail_id { get; set; }

    public Guid billing_id { get; set; }

    public string description { get; set; } = null!;

    public int quantity { get; set; }

    public decimal unit_price { get; set; }

    public decimal? amount { get; set; }

    public virtual billing billing { get; set; } = null!;
}
