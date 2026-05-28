using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class vital_sign
{
    public Guid vital_sign_id { get; set; }

    public Guid medical_record_id { get; set; }

    public Guid? recorded_by { get; set; }

    public int? systolic_bp { get; set; }

    public int? diastolic_bp { get; set; }

    public int? heart_rate { get; set; }

    public decimal? temperature { get; set; }

    public int? respiratory_rate { get; set; }

    public decimal? weight_kg { get; set; }

    public decimal? height_cm { get; set; }

    public int? spo2 { get; set; }

    public DateTime? recorded_at { get; set; }

    public virtual medical_record medical_record { get; set; } = null!;

    public virtual user? recorded_byNavigation { get; set; }
}
