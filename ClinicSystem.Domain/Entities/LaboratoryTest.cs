using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class LaboratoryTest
{
    public Guid LaboratoryTestId { get; set; }

    public Guid PatientId { get; set; }

    public Guid DoctorId { get; set; }

    public Guid? MedicalRecordId { get; set; }

    public string TestName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? RequestedDate { get; set; }

    public DateTime? SampleTakenDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public string? Observations { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<LaboratoryResult> LaboratoryResults { get; set; } = new List<LaboratoryResult>();

    public virtual MedicalRecord? MedicalRecord { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
