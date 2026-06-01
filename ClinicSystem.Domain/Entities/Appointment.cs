using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Appointment
{
    public Guid AppointmentId { get; set; }

    public Guid PatientId { get; set; }

    public Guid DoctorId { get; set; }

    public Guid StatusId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public int? DurationMinutes { get; set; }

    public string? Reason { get; set; }

    public string? Notes { get; set; }

    public string? CancellationReason { get; set; }

    public Guid? RescheduledFrom { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<Appointment> InverseRescheduledFromNavigation { get; set; } = new List<Appointment>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual Appointment? RescheduledFromNavigation { get; set; }

    public virtual AppointmentStatus Status { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
