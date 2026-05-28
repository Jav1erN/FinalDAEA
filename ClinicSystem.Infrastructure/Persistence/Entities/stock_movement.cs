using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class stock_movement
{
    public Guid movement_id { get; set; }

    public Guid medication_id { get; set; }

    public string movement_type { get; set; } = null!;

    public int quantity { get; set; }

    public Guid? reference_id { get; set; }

    public string? notes { get; set; }

    public Guid? performed_by { get; set; }

    public DateTime? created_at { get; set; }

    public virtual medication medication { get; set; } = null!;

    public virtual user? performed_byNavigation { get; set; }
}
