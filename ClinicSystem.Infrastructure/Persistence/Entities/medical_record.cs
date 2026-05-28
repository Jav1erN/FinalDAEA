using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class medical_record
{
    public Guid medical_record_id { get; set; }

    public Guid patient_id { get; set; }

    public Guid doctor_id { get; set; }

    public Guid? appointment_id { get; set; }

    public string? chief_complaint { get; set; }

    public string? diagnosis { get; set; }

    public string? treatment { get; set; }

    public string? observations { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public Guid? created_by { get; set; }

    public Guid? updated_by { get; set; }

    public virtual appointment? appointment { get; set; }

    public virtual user? created_byNavigation { get; set; }

    public virtual ICollection<diagnosis> diagnoses { get; set; } = new List<diagnosis>();

    public virtual doctor doctor { get; set; } = null!;

    public virtual ICollection<laboratory_test> laboratory_tests { get; set; } = new List<laboratory_test>();

    public virtual patient patient { get; set; } = null!;

    public virtual ICollection<prescription> prescriptions { get; set; } = new List<prescription>();

    public virtual ICollection<treatment> treatments { get; set; } = new List<treatment>();

    public virtual user? updated_byNavigation { get; set; }

    public virtual ICollection<vital_sign> vital_signs { get; set; } = new List<vital_sign>();
}
