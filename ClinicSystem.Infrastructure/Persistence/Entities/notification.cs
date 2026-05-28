using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class notification
{
    public Guid notification_id { get; set; }

    public Guid user_id { get; set; }

    public Guid type_id { get; set; }

    public string channel { get; set; } = null!;

    public string? status { get; set; }

    public string? entity_type { get; set; }

    public Guid? entity_id { get; set; }

    public string? subject { get; set; }

    public string? body { get; set; }

    public DateTime? scheduled_at { get; set; }

    public DateTime? sent_at { get; set; }

    public DateTime? read_at { get; set; }

    public DateTime? created_at { get; set; }

    public virtual notification_type type { get; set; } = null!;

    public virtual user user { get; set; } = null!;
}
