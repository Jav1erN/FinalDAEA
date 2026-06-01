using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Treatment
{
    public Guid TreatmentId { get; set; }

    public Guid MedicalRecordId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
