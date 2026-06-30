using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.InsurancePolicies.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsurancePolicies.Queries;

public record GetInsurancePoliciesQuery : IRequest<Result<IEnumerable<InsurancePolicyDto>>>;

public class GetInsurancePoliciesQueryHandler
    : IRequestHandler<GetInsurancePoliciesQuery, Result<IEnumerable<InsurancePolicyDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInsurancePoliciesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<InsurancePolicyDto>>> Handle(
        GetInsurancePoliciesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.InsurancePolicies
            .ListAsync(cancellationToken);

        return Result<IEnumerable<InsurancePolicyDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

