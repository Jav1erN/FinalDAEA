using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class PrescriptionDetail
{
    public Guid PrescriptionDetailId { get; set; }

    public Guid PrescriptionId { get; set; }

    public Guid MedicationId { get; set; }

    public string? Dosage { get; set; }

    public string? Frequency { get; set; }

    public int? DurationDays { get; set; }

    public int QuantityPrescribed { get; set; }

    public string? Instructions { get; set; }

    public bool? IsSubstitutable { get; set; }

    public virtual Medication Medication { get; set; } = null!;

    public virtual Prescription Prescription { get; set; } = null!;
}
