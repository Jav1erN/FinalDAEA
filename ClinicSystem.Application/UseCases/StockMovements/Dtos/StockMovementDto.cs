namespace ClinicSystem.Application.UseCases.StockMovements.Dtos;

public class StockMovementDto
{
    public Guid MovementId { get; set; } = Guid.Empty;

    public Guid MedicationId { get; set; } = Guid.Empty;

    public string MovementType { get; set; } = string.Empty;

    public int Quantity { get; set; } = 0;

    public Guid? ReferenceId { get; set; } = null;

    public string? Notes { get; set; } = null;

    public Guid? PerformedBy { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;
}
