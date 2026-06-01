using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.BillingDetails.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.BillingDetails.Queries;

public record GetBillingDetailByIdQuery(Guid BillingDetailId) : IRequest<Result<BillingDetailDto>>;

public class GetBillingDetailByIdQueryHandler
    : IRequestHandler<GetBillingDetailByIdQuery, Result<BillingDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBillingDetailByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BillingDetailDto>> Handle(
        GetBillingDetailByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<BillingDetail>()
            .GetByIdAsync(request.BillingDetailId, cancellationToken);

        if (entity is null)
            return Result<BillingDetailDto>.Failure("BillingDetail not found");

        return Result<BillingDetailDto>.Success(entity.ToDto());
    }
}
