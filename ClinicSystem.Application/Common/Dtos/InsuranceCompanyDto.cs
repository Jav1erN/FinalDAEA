namespace ClinicSystem.Application.Common.Dtos;

public class InsuranceCompanyDto
{
    public Guid InsuranceCompanyId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Phone { get; set; } = null;

    public string? Email { get; set; } = null;

    public string? Address { get; set; } = null;

    public string? ContactName { get; set; } = null;

    public bool? IsActive { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;
}

