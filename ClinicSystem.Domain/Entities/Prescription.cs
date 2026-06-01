using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Prescription
{
    public Guid PrescriptionId { get; set; }

    public Guid MedicalRecordId { get; set; }

    public Guid DoctorId { get; set; }

    public Guid PatientId { get; set; }

    public DateOnly? ValidUntil { get; set; }

    public DateTime? DispensedAt { get; set; }

    public Guid? DispensedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? DispensedByNavigation { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();

    public virtual User? UpdatedByNavigation { get; set; }
}
