using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class NotificationType
{
    public Guid TypeId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? TemplateSubject { get; set; }

    public string? TemplateBody { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
