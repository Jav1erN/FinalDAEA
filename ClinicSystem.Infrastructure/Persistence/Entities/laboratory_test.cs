using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class laboratory_test
{
    public Guid laboratory_test_id { get; set; }

    public Guid patient_id { get; set; }

    public Guid doctor_id { get; set; }

    public Guid? medical_record_id { get; set; }

    public string test_name { get; set; } = null!;

    public string status { get; set; } = null!;

    public DateTime? requested_date { get; set; }

    public DateTime? sample_taken_date { get; set; }

    public DateTime? completed_date { get; set; }

    public string? observations { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public Guid? created_by { get; set; }

    public Guid? updated_by { get; set; }

    public virtual user? created_byNavigation { get; set; }

    public virtual doctor doctor { get; set; } = null!;

    public virtual ICollection<laboratory_result> laboratory_results { get; set; } = new List<laboratory_result>();

    public virtual medical_record? medical_record { get; set; }

    public virtual patient patient { get; set; } = null!;

    public virtual user? updated_byNavigation { get; set; }
}
