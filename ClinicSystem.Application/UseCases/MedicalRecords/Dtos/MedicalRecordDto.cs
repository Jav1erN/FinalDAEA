namespace ClinicSystem.Application.UseCases.MedicalRecords.Dtos;

public class MedicalRecordDto
{
    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid? AppointmentId { get; set; } = null;

    public string? ChiefComplaint { get; set; } = null;

    public string? Diagnosis { get; set; } = null;

    public string? Treatment { get; set; } = null;

    public string? Observations { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime? DeletedAt { get; set; } = null;

    public Guid? CreatedBy { get; set; } = null;

    public Guid? UpdatedBy { get; set; } = null;
}
