namespace ClinicSystem.Application.Common.Dtos;

public class InsurancePolicyDto
{
    public Guid InsurancePolicyId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid InsuranceCompanyId { get; set; } = Guid.Empty;

    public string PolicyNumber { get; set; } = string.Empty;

    public decimal? CoveragePercentage { get; set; } = null;

    public decimal? MaxCoverageAmount { get; set; } = null;

    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public DateOnly? EndDate { get; set; } = null;

    public bool? IsActive { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime? DeletedAt { get; set; } = null;

    public Guid? CreatedBy { get; set; } = null;

    public Guid? UpdatedBy { get; set; } = null;
}

