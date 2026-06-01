using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class VitalSign
{
    public Guid VitalSignId { get; set; }

    public Guid MedicalRecordId { get; set; }

    public Guid? RecordedBy { get; set; }

    public int? SystolicBp { get; set; }

    public int? DiastolicBp { get; set; }

    public int? HeartRate { get; set; }

    public decimal? Temperature { get; set; }

    public int? RespiratoryRate { get; set; }

    public decimal? WeightKg { get; set; }

    public decimal? HeightCm { get; set; }

    public int? Spo2 { get; set; }

    public DateTime? RecordedAt { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual User? RecordedByNavigation { get; set; }
}
