using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class notification_type
{
    public Guid type_id { get; set; }

    public string code { get; set; } = null!;

    public string name { get; set; } = null!;

    public string? template_subject { get; set; }

    public string? template_body { get; set; }

    public bool? is_active { get; set; }

    public virtual ICollection<notification> notifications { get; set; } = new List<notification>();
}
