using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Common.Dtos;

public static class StockMovementMappings
{
    public static StockMovementDto ToDto(this StockMovement entity)
    {
        return new StockMovementDto
        {
            MovementId = entity.MovementId,
            MedicationId = entity.MedicationId,
            MovementType = entity.MovementType,
            Quantity = entity.Quantity,
            ReferenceId = entity.ReferenceId,
            Notes = entity.Notes,
            PerformedBy = entity.PerformedBy,
            CreatedAt = entity.CreatedAt
        };
    }
}

