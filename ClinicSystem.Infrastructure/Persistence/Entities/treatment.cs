using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class treatment
{
    public Guid treatment_id { get; set; }

    public Guid medical_record_id { get; set; }

    public string description { get; set; } = null!;

    public DateOnly? start_date { get; set; }

    public DateOnly? end_date { get; set; }

    public string? status { get; set; }

    public string? notes { get; set; }

    public virtual medical_record medical_record { get; set; } = null!;
}
