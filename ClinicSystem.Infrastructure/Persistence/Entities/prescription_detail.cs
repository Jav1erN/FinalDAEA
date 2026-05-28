using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class prescription_detail
{
    public Guid prescription_detail_id { get; set; }

    public Guid prescription_id { get; set; }

    public Guid medication_id { get; set; }

    public string? dosage { get; set; }

    public string? frequency { get; set; }

    public int? duration_days { get; set; }

    public int quantity_prescribed { get; set; }

    public string? instructions { get; set; }

    public bool? is_substitutable { get; set; }

    public virtual medication medication { get; set; } = null!;

    public virtual prescription prescription { get; set; } = null!;
}
