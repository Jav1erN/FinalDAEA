using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class InsurancePolicy
{
    public Guid InsurancePolicyId { get; set; }

    public Guid PatientId { get; set; }

    public Guid InsuranceCompanyId { get; set; }

    public string PolicyNumber { get; set; } = null!;

    public decimal? CoveragePercentage { get; set; }

    public decimal? MaxCoverageAmount { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual InsuranceCompany InsuranceCompany { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User? UpdatedByNavigation { get; set; }
}
