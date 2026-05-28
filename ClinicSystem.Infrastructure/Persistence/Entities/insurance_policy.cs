using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class insurance_policy
{
    public Guid insurance_policy_id { get; set; }

    public Guid patient_id { get; set; }

    public Guid insurance_company_id { get; set; }

    public string policy_number { get; set; } = null!;

    public decimal? coverage_percentage { get; set; }

    public decimal? max_coverage_amount { get; set; }

    public DateOnly start_date { get; set; }

    public DateOnly? end_date { get; set; }

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public Guid? created_by { get; set; }

    public Guid? updated_by { get; set; }

    public virtual ICollection<billing> billings { get; set; } = new List<billing>();

    public virtual user? created_byNavigation { get; set; }

    public virtual insurance_company insurance_company { get; set; } = null!;

    public virtual patient patient { get; set; } = null!;

    public virtual ICollection<payment> payments { get; set; } = new List<payment>();

    public virtual user? updated_byNavigation { get; set; }
}
