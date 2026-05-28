using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class patient
{
    public Guid patient_id { get; set; }

    public Guid? user_id { get; set; }

    public string document_number { get; set; } = null!;

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public DateOnly? birth_date { get; set; }

    public string? gender { get; set; }

    public string? blood_type { get; set; }

    public string? phone { get; set; }

    public string? email { get; set; }

    public string? address { get; set; }

    public string? emergency_contact_name { get; set; }

    public string? emergency_contact_phone { get; set; }

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public Guid? created_by { get; set; }

    public Guid? updated_by { get; set; }

    public virtual ICollection<appointment> appointments { get; set; } = new List<appointment>();

    public virtual ICollection<billing> billings { get; set; } = new List<billing>();

    public virtual user? created_byNavigation { get; set; }

    public virtual ICollection<insurance_policy> insurance_policies { get; set; } = new List<insurance_policy>();

    public virtual ICollection<laboratory_test> laboratory_tests { get; set; } = new List<laboratory_test>();

    public virtual ICollection<medical_record> medical_records { get; set; } = new List<medical_record>();

    public virtual ICollection<prescription> prescriptions { get; set; } = new List<prescription>();

    public virtual user? updated_byNavigation { get; set; }

    public virtual user? user { get; set; }
}
