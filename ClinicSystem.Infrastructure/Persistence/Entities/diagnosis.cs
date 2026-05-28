using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class diagnosis
{
    public Guid diagnosis_id { get; set; }

    public Guid medical_record_id { get; set; }

    public string cie10_code { get; set; } = null!;

    public string? description { get; set; }

    public bool? is_primary { get; set; }

    public DateTime? noted_at { get; set; }

    public virtual medical_record medical_record { get; set; } = null!;
}
