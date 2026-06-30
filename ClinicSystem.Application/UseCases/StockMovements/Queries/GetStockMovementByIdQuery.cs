using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.StockMovements.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.StockMovements.Queries;

public record GetStockMovementByIdQuery(Guid MovementId) : IRequest<Result<StockMovementDto>>;

public class GetStockMovementByIdQueryHandler
    : IRequestHandler<GetStockMovementByIdQuery, Result<StockMovementDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStockMovementByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<StockMovementDto>> Handle(
        GetStockMovementByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.StockMovements
            .GetByIdAsync(request.MovementId, cancellationToken);

        if (entity is null)
            return Result<StockMovementDto>.Failure("StockMovement not found");

        return Result<StockMovementDto>.Success(entity.ToDto());
    }
}

