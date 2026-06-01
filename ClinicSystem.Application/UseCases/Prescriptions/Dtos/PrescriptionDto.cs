namespace ClinicSystem.Application.UseCases.Prescriptions.Dtos;

public class PrescriptionDto
{
    public Guid PrescriptionId { get; set; } = Guid.Empty;

    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public DateOnly? ValidUntil { get; set; } = null;

    public DateTime? DispensedAt { get; set; } = null;

    public Guid? DispensedBy { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime? DeletedAt { get; set; } = null;

    public Guid? CreatedBy { get; set; } = null;

    public Guid? UpdatedBy { get; set; } = null;
}
