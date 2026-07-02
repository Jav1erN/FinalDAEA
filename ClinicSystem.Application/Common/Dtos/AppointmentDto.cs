namespace ClinicSystem.Application.Common.Dtos;

public class AppointmentDto
{
    public Guid AppointmentId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid StatusId { get; set; } = Guid.Empty;

    public DateTime AppointmentDate { get; set; } = DateTime.UtcNow;

    public int? DurationMinutes { get; set; } = null;

    public string? Reason { get; set; } = null;

    public string? Notes { get; set; } = null;

    public string? CancellationReason { get; set; } = null;

    public Guid? RescheduledFrom { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime? DeletedAt { get; set; } = null;

    public Guid? CreatedBy { get; set; } = null;

    public Guid? UpdatedBy { get; set; } = null;
}

