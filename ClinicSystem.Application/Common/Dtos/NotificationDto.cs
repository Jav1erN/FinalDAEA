namespace ClinicSystem.Application.Common.Dtos;

public class NotificationDto
{
    public Guid NotificationId { get; set; } = Guid.Empty;

    public Guid UserId { get; set; } = Guid.Empty;

    public Guid TypeId { get; set; } = Guid.Empty;

    public string Channel { get; set; } = string.Empty;

    public string? Status { get; set; } = null;

    public string? EntityType { get; set; } = null;

    public Guid? EntityId { get; set; } = null;

    public string? Subject { get; set; } = null;

    public string? Body { get; set; } = null;

    public DateTime? ScheduledAt { get; set; } = null;

    public DateTime? SentAt { get; set; } = null;

    public DateTime? ReadAt { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;
}

