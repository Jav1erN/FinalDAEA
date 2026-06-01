using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.InsurancePolicies.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsurancePolicies.Queries;

public record GetInsurancePolicyByIdQuery(Guid InsurancePolicyId) : IRequest<Result<InsurancePolicyDto>>;

public class GetInsurancePolicyByIdQueryHandler
    : IRequestHandler<GetInsurancePolicyByIdQuery, Result<InsurancePolicyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInsurancePolicyByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<InsurancePolicyDto>> Handle(
        GetInsurancePolicyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<InsurancePolicy>()
            .GetByIdAsync(request.InsurancePolicyId, cancellationToken);

        if (entity is null)
            return Result<InsurancePolicyDto>.Failure("InsurancePolicy not found");

        return Result<InsurancePolicyDto>.Success(entity.ToDto());
    }
}
