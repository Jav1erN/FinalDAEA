using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class refresh_token
{
    public Guid refresh_token_id { get; set; }

    public Guid user_id { get; set; }

    public string token_hash { get; set; } = null!;

    public DateTime expires_at { get; set; }

    public DateTime? revoked_at { get; set; }

    public DateTime? created_at { get; set; }

    public virtual user user { get; set; } = null!;
}
