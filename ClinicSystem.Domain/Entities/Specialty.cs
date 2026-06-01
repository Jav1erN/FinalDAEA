using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Specialty
{
    public Guid SpecialtyId { get; set; }

    public Guid DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
