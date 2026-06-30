using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.StockMovements.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.StockMovements.Commands;

public record UpdateStockMovementCommand(
    Guid MovementId,
    Guid MedicationId,
    string MovementType,
    int Quantity,
    Guid? ReferenceId,
    string? Notes,
    Guid? PerformedBy
) : IRequest<Result<StockMovementDto>>;

public class UpdateStockMovementCommandHandler
    : IRequestHandler<UpdateStockMovementCommand, Result<StockMovementDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStockMovementCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<StockMovementDto>> Handle(
        UpdateStockMovementCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.StockMovements;
        var entity = await repository.GetByIdAsync(request.MovementId, cancellationToken);

        if (entity is null)
            return Result<StockMovementDto>.Failure("StockMovement not found");

        entity.MedicationId = request.MedicationId;
        entity.MovementType = request.MovementType;
        entity.Quantity = request.Quantity;
        entity.ReferenceId = request.ReferenceId;
        entity.Notes = request.Notes;
        entity.PerformedBy = request.PerformedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<StockMovementDto>.Success(entity.ToDto());
    }
}

