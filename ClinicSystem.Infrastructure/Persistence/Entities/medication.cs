using System;
using System.Collections.Generic;

namespace ClinicSystem.Infrastructure.Persistence.Entities;

public partial class medication
{
    public Guid medication_id { get; set; }

    public string name { get; set; } = null!;

    public string? generic_name { get; set; }

    public string? presentation { get; set; }

    public string? concentration { get; set; }

    public string? laboratory { get; set; }

    public bool? requires_prescription { get; set; }

    public int? stock { get; set; }

    public decimal? unit_price { get; set; }

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public Guid? created_by { get; set; }

    public Guid? updated_by { get; set; }

    public virtual user? created_byNavigation { get; set; }

    public virtual ICollection<prescription_detail> prescription_details { get; set; } = new List<prescription_detail>();

    public virtual ICollection<stock_movement> stock_movements { get; set; } = new List<stock_movement>();

    public virtual user? updated_byNavigation { get; set; }
}
