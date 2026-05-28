using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class specialty
{
    public Guid specialty_id { get; set; }

    public Guid department_id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual department department { get; set; } = null!;

    public virtual ICollection<doctor> doctors { get; set; } = new List<doctor>();
}
