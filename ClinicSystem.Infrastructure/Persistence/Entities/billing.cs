using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class billing
{
    public Guid billing_id { get; set; }

    public Guid patient_id { get; set; }

    public Guid? appointment_id { get; set; }

    public Guid? insurance_policy_id { get; set; }

    public DateTime? issue_date { get; set; }

    public decimal subtotal { get; set; }

    public decimal? discount { get; set; }

    public decimal? insurance_coverage { get; set; }

    public decimal? total_amount { get; set; }

    public string status { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public Guid? created_by { get; set; }

    public Guid? updated_by { get; set; }

    public virtual appointment? appointment { get; set; }

    public virtual ICollection<billing_detail> billing_details { get; set; } = new List<billing_detail>();

    public virtual user? created_byNavigation { get; set; }

    public virtual insurance_policy? insurance_policy { get; set; }

    public virtual patient patient { get; set; } = null!;

    public virtual ICollection<payment> payments { get; set; } = new List<payment>();

    public virtual user? updated_byNavigation { get; set; }
}
