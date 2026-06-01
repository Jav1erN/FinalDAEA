using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class Medication
{
    public Guid MedicationId { get; set; }

    public string Name { get; set; } = null!;

    public string? GenericName { get; set; }

    public string? Presentation { get; set; }

    public string? Concentration { get; set; }

    public string? Laboratory { get; set; }

    public bool? RequiresPrescription { get; set; }

    public int? Stock { get; set; }

    public decimal? UnitPrice { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    public virtual User? UpdatedByNavigation { get; set; }
}
