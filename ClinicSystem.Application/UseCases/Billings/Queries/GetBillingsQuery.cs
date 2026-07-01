using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Billings.Queries;

public record GetBillingsQuery : IRequest<Result<IEnumerable<BillingDto>>>;

public class GetBillingsQueryHandler
    : IRequestHandler<GetBillingsQuery, Result<IEnumerable<BillingDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBillingsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<BillingDto>>> Handle(
        GetBillingsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Billings
            .ListAsync(cancellationToken);

        return Result<IEnumerable<BillingDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

