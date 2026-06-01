using System;
using System.Collections.Generic;
using System.Net;

namespace ClinicSystem.Domain.Entities;

public partial class AuditLog
{
    public Guid AuditLogId { get; set; }

    public Guid? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string EntityName { get; set; } = null!;

    public Guid? EntityId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public IPAddress? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public Guid? CorrelationId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
