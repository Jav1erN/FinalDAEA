namespace ClinicSystem.Application.UseCases.AuditLogs.Dtos;

public class AuditLogDto
{
    public Guid AuditLogId { get; set; } = Guid.Empty;

    public Guid? UserId { get; set; } = null;

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public Guid? EntityId { get; set; } = null;

    public string? OldValues { get; set; } = null;

    public string? NewValues { get; set; } = null;

    public string? UserAgent { get; set; } = null;

    public Guid? CorrelationId { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;
}
