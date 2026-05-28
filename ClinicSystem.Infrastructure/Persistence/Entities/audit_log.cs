using System;
using System.Collections.Generic;
using System.Net;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class audit_log
{
    public Guid audit_log_id { get; set; }

    public Guid? user_id { get; set; }

    public string action { get; set; } = null!;

    public string entity_name { get; set; } = null!;

    public Guid? entity_id { get; set; }

    public string? old_values { get; set; }

    public string? new_values { get; set; }

    public IPAddress? ip_address { get; set; }

    public string? user_agent { get; set; }

    public Guid? correlation_id { get; set; }

    public DateTime? created_at { get; set; }

    public virtual user? user { get; set; }
}
