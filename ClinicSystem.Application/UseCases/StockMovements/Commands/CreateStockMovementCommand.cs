using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.StockMovements.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.StockMovements.Commands;

public record CreateStockMovementCommand(
    Guid MedicationId,
    string MovementType,
    int Quantity,
    Guid? ReferenceId,
    string? Notes,
    Guid? PerformedBy
) : IRequest<Result<StockMovementDto>>;

public class CreateStockMovementCommandHandler
    : IRequestHandler<CreateStockMovementCommand, Result<StockMovementDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateStockMovementCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<StockMovementDto>> Handle(
        CreateStockMovementCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new StockMovement
        {
            MovementId = Guid.NewGuid(),
            MedicationId = request.MedicationId,
            MovementType = request.MovementType,
            Quantity = request.Quantity,
            ReferenceId = request.ReferenceId,
            Notes = request.Notes,
            PerformedBy = request.PerformedBy
        };

        await _unitOfWork.Repository<StockMovement>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<StockMovementDto>.Success(entity.ToDto());
    }
}
