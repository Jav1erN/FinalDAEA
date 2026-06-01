using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.InsuranceCompanies.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsuranceCompanies.Queries;

public record GetInsuranceCompanyByIdQuery(Guid InsuranceCompanyId) : IRequest<Result<InsuranceCompanyDto>>;

public class GetInsuranceCompanyByIdQueryHandler
    : IRequestHandler<GetInsuranceCompanyByIdQuery, Result<InsuranceCompanyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInsuranceCompanyByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<InsuranceCompanyDto>> Handle(
        GetInsuranceCompanyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<InsuranceCompany>()
            .GetByIdAsync(request.InsuranceCompanyId, cancellationToken);

        if (entity is null)
            return Result<InsuranceCompanyDto>.Failure("InsuranceCompany not found");

        return Result<InsuranceCompanyDto>.Success(entity.ToDto());
    }
}
