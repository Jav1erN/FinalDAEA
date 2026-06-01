using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Permission
{
    public Guid PermissionId { get; set; }

    public string Resource { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
