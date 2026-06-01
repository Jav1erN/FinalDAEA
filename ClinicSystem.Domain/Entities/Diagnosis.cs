using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Diagnosis
{
    public Guid DiagnosisId { get; set; }

    public Guid MedicalRecordId { get; set; }

    public string Cie10Code { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsPrimary { get; set; }

    public DateTime? NotedAt { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
