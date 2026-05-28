using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class payment
{
    public Guid payment_id { get; set; }

    public Guid billing_id { get; set; }

    public Guid? insurance_policy_id { get; set; }

    public decimal amount { get; set; }

    public string payment_method { get; set; } = null!;

    public string? reference_number { get; set; }

    public DateTime? payment_date { get; set; }

    public string? status { get; set; }

    public Guid? registered_by { get; set; }

    public DateTime? created_at { get; set; }

    public virtual billing billing { get; set; } = null!;

    public virtual insurance_policy? insurance_policy { get; set; }

    public virtual user? registered_byNavigation { get; set; }
}
