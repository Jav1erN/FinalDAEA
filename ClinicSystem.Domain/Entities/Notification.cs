using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Notification
{
    public Guid NotificationId { get; set; }

    public Guid UserId { get; set; }

    public Guid TypeId { get; set; }

    public string Channel { get; set; } = null!;

    public string? Status { get; set; }

    public string? EntityType { get; set; }

    public Guid? EntityId { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public DateTime? ScheduledAt { get; set; }

    public DateTime? SentAt { get; set; }

    public DateTime? ReadAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual NotificationType Type { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
