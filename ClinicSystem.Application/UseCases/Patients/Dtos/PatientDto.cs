namespace ClinicSystem.Application.UseCases.Patients.Dtos;

public class PatientDto
{
    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid? UserId { get; set; } = null;

    public string DocumentNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly? BirthDate { get; set; } = null;

    public string? Gender { get; set; } = null;

    public string? BloodType { get; set; } = null;

    public string? Phone { get; set; } = null;

    public string? Email { get; set; } = null;

    public string? Address { get; set; } = null;

    public string? EmergencyContactName { get; set; } = null;

    public string? EmergencyContactPhone { get; set; } = null;

    public bool? IsActive { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime? DeletedAt { get; set; } = null;

    public Guid? CreatedBy { get; set; } = null;

    public Guid? UpdatedBy { get; set; } = null;
}
