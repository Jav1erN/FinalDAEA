using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.StockMovements.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.StockMovements.Queries;

public record GetStockMovementsQuery : IRequest<Result<IEnumerable<StockMovementDto>>>;

public class GetStockMovementsQueryHandler
    : IRequestHandler<GetStockMovementsQuery, Result<IEnumerable<StockMovementDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetStockMovementsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<StockMovementDto>>> Handle(
        GetStockMovementsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<StockMovement>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<StockMovementDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
