using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class role
{
    public Guid role_id { get; set; }

    public string name { get; set; } = null!;

    public string? description { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<user> users { get; set; } = new List<user>();

    public virtual ICollection<permission> permissions { get; set; } = new List<permission>();
}
