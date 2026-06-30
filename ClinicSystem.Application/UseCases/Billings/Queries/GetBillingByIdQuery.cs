using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Billings.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Billings.Queries;

public record GetBillingByIdQuery(Guid BillingId) : IRequest<Result<BillingDto>>;

public class GetBillingByIdQueryHandler
    : IRequestHandler<GetBillingByIdQuery, Result<BillingDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBillingByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BillingDto>> Handle(
        GetBillingByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Billings
            .GetByIdAsync(request.BillingId, cancellationToken);

        if (entity is null)
            return Result<BillingDto>.Failure("Billing not found");

        return Result<BillingDto>.Success(entity.ToDto());
    }
}

