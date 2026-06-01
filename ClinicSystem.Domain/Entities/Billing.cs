using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Billing
{
    public Guid BillingId { get; set; }

    public Guid PatientId { get; set; }

    public Guid? AppointmentId { get; set; }

    public Guid? InsurancePolicyId { get; set; }

    public DateTime? IssueDate { get; set; }

    public decimal Subtotal { get; set; }

    public decimal? Discount { get; set; }

    public decimal? InsuranceCoverage { get; set; }

    public decimal? TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual ICollection<BillingDetail> BillingDetails { get; set; } = new List<BillingDetail>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual InsurancePolicy? InsurancePolicy { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User? UpdatedByNavigation { get; set; }
}
