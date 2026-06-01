namespace ClinicSystem.Application.UseCases.Doctors.Dtos;

public class DoctorDto
{
    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid UserId { get; set; } = Guid.Empty;

    public Guid SpecialtyId { get; set; } = Guid.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public int? YearsExperience { get; set; } = null;

    public decimal? ConsultationFee { get; set; } = null;

    public string? Office { get; set; } = null;

    public bool? IsActive { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime? DeletedAt { get; set; } = null;

    public Guid? CreatedBy { get; set; } = null;

    public Guid? UpdatedBy { get; set; } = null;
}
