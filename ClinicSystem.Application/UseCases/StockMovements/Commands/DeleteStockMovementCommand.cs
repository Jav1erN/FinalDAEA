using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.StockMovements.Commands;

public class DeleteStockMovementCommand : IRequest<Result<bool>>
{
    public Guid MovementId { get; set; } = Guid.Empty;
}

public class DeleteStockMovementCommandHandler
    : IRequestHandler<DeleteStockMovementCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStockMovementCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteStockMovementCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.StockMovements;
        var entity = await repository.GetByIdAsync(request.MovementId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("StockMovement not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

