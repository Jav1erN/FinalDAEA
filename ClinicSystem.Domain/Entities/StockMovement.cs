using System;
using System.Collections.Generic;

namespace ClinicSystem.Domain.Entities;

public partial class StockMovement
{
    public Guid MovementId { get; set; }

    public Guid MedicationId { get; set; }

    public string MovementType { get; set; } = null!;

    public int Quantity { get; set; }

    public Guid? ReferenceId { get; set; }

    public string? Notes { get; set; }

    public Guid? PerformedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Medication Medication { get; set; } = null!;

    public virtual User? PerformedByNavigation { get; set; }
}
