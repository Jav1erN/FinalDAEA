using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.InsuranceCompanies.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsuranceCompanies.Queries;

public record GetInsuranceCompaniesQuery : IRequest<Result<IEnumerable<InsuranceCompanyDto>>>;

public class GetInsuranceCompaniesQueryHandler
    : IRequestHandler<GetInsuranceCompaniesQuery, Result<IEnumerable<InsuranceCompanyDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInsuranceCompaniesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<InsuranceCompanyDto>>> Handle(
        GetInsuranceCompaniesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.InsuranceCompanies
            .ListAsync(cancellationToken);

        return Result<IEnumerable<InsuranceCompanyDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

