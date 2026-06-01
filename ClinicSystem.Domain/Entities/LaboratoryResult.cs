using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class LaboratoryResult
{
    public Guid ResultId { get; set; }

    public Guid LaboratoryTestId { get; set; }

    public string ParameterName { get; set; } = null!;

    public string? ResultValue { get; set; }

    public string? Unit { get; set; }

    public string? ReferenceRange { get; set; }

    public bool? IsAbnormal { get; set; }

    public DateTime? NotedAt { get; set; }

    public virtual LaboratoryTest LaboratoryTest { get; set; } = null!;
}
