namespace ClinicSystem.Application.UseCases.LaboratoryResults.Dtos;

public class LaboratoryResultDto
{
    public Guid ResultId { get; set; } = Guid.Empty;

    public Guid LaboratoryTestId { get; set; } = Guid.Empty;

    public string ParameterName { get; set; } = string.Empty;

    public string? ResultValue { get; set; } = null;

    public string? Unit { get; set; } = null;

    public string? ReferenceRange { get; set; } = null;

    public bool? IsAbnormal { get; set; } = null;

    public DateTime? NotedAt { get; set; } = null;
}

