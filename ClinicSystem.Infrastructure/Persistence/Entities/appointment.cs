using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class appointment
{
    public Guid appointment_id { get; set; }

    public Guid patient_id { get; set; }

    public Guid doctor_id { get; set; }

    public Guid status_id { get; set; }

    public DateTime appointment_date { get; set; }

    public int? duration_minutes { get; set; }

    public string? reason { get; set; }

    public string? notes { get; set; }

    public string? cancellation_reason { get; set; }

    public Guid? rescheduled_from { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public Guid? created_by { get; set; }

    public Guid? updated_by { get; set; }

    public virtual ICollection<appointment> Inverserescheduled_fromNavigation { get; set; } = new List<appointment>();

    public virtual ICollection<billing> billings { get; set; } = new List<billing>();

    public virtual user? created_byNavigation { get; set; }

    public virtual doctor doctor { get; set; } = null!;

    public virtual ICollection<medical_record> medical_records { get; set; } = new List<medical_record>();

    public virtual patient patient { get; set; } = null!;

    public virtual appointment? rescheduled_fromNavigation { get; set; }

    public virtual appointment_status status { get; set; } = null!;

    public virtual user? updated_byNavigation { get; set; }
}
