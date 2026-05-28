using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class laboratory_result
{
    public Guid result_id { get; set; }

    public Guid laboratory_test_id { get; set; }

    public string parameter_name { get; set; } = null!;

    public string? result_value { get; set; }

    public string? unit { get; set; }

    public string? reference_range { get; set; }

    public bool? is_abnormal { get; set; }

    public DateTime? noted_at { get; set; }

    public virtual laboratory_test laboratory_test { get; set; } = null!;
}
