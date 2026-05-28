using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class department
{
    public Guid department_id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<specialty> specialties { get; set; } = new List<specialty>();
}
