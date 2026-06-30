using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.BillingDetails.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.BillingDetails.Queries;

public record GetBillingDetailsQuery : IRequest<Result<IEnumerable<BillingDetailDto>>>;

public class GetBillingDetailsQueryHandler
    : IRequestHandler<GetBillingDetailsQuery, Result<IEnumerable<BillingDetailDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBillingDetailsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<BillingDetailDto>>> Handle(
        GetBillingDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.BillingDetails
            .ListAsync(cancellationToken);

        return Result<IEnumerable<BillingDetailDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

