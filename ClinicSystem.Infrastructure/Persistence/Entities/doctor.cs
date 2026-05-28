using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class doctor
{
    public Guid doctor_id { get; set; }

    public Guid user_id { get; set; }

    public Guid specialty_id { get; set; }

    public string license_number { get; set; } = null!;

    public int? years_experience { get; set; }

    public decimal? consultation_fee { get; set; }

    public string? office { get; set; }

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public Guid? created_by { get; set; }

    public Guid? updated_by { get; set; }

    public virtual ICollection<appointment> appointments { get; set; } = new List<appointment>();

    public virtual user? created_byNavigation { get; set; }

    public virtual ICollection<laboratory_test> laboratory_tests { get; set; } = new List<laboratory_test>();

    public virtual ICollection<medical_record> medical_records { get; set; } = new List<medical_record>();

    public virtual ICollection<prescription> prescriptions { get; set; } = new List<prescription>();

    public virtual specialty specialty { get; set; } = null!;

    public virtual user? updated_byNavigation { get; set; }

    public virtual user user { get; set; } = null!;
}
