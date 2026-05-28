using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class prescription
{
    public Guid prescription_id { get; set; }

    public Guid medical_record_id { get; set; }

    public Guid doctor_id { get; set; }

    public Guid patient_id { get; set; }

    public DateOnly? valid_until { get; set; }

    public DateTime? dispensed_at { get; set; }

    public Guid? dispensed_by { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public Guid? created_by { get; set; }

    public Guid? updated_by { get; set; }

    public virtual user? created_byNavigation { get; set; }

    public virtual user? dispensed_byNavigation { get; set; }

    public virtual doctor doctor { get; set; } = null!;

    public virtual medical_record medical_record { get; set; } = null!;

    public virtual patient patient { get; set; } = null!;

    public virtual ICollection<prescription_detail> prescription_details { get; set; } = new List<prescription_detail>();

    public virtual user? updated_byNavigation { get; set; }
}
