using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Role
{
    public Guid RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
