using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.StockMovements.Commands;

public record DeleteStockMovementCommand(Guid MovementId) : IRequest<Result<bool>>;

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
        var repository = _unitOfWork.Repository<StockMovement>();
        var entity = await repository.GetByIdAsync(request.MovementId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("StockMovement not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
