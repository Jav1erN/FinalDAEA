using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class permission
{
    public Guid permission_id { get; set; }

    public string resource { get; set; } = null!;

    public string action { get; set; } = null!;

    public string? description { get; set; }

    public DateTime? created_at { get; set; }

    public virtual ICollection<role> roles { get; set; } = new List<role>();
}
