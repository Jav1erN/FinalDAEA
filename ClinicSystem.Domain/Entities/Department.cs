using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Department
{
    public Guid DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Specialty> Specialties { get; set; } = new List<Specialty>();
}
