using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class insurance_company
{
    public Guid insurance_company_id { get; set; }

    public string name { get; set; } = null!;

    public string? phone { get; set; }

    public string? email { get; set; }

    public string? address { get; set; }

    public string? contact_name { get; set; }

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<insurance_policy> insurance_policies { get; set; } = new List<insurance_policy>();
}
