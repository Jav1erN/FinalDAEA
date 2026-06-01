using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Payment
{
    public Guid PaymentId { get; set; }

    public Guid BillingId { get; set; }

    public Guid? InsurancePolicyId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? ReferenceNumber { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Status { get; set; }

    public Guid? RegisteredBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Billing Billing { get; set; } = null!;

    public virtual InsurancePolicy? InsurancePolicy { get; set; }

    public virtual User? RegisteredByNavigation { get; set; }
}
