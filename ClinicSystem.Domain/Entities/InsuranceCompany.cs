using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class InsuranceCompany
{
    public Guid InsuranceCompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? ContactName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<InsurancePolicy> InsurancePolicies { get; set; } = new List<InsurancePolicy>();
}
