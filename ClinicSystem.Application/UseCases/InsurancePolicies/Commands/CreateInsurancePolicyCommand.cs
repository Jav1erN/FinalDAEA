using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.InsurancePolicies.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsurancePolicies.Commands;

public record CreateInsurancePolicyCommand(
    Guid PatientId,
    Guid InsuranceCompanyId,
    string PolicyNumber,
    decimal? CoveragePercentage,
    decimal? MaxCoverageAmount,
    DateOnly StartDate,
    DateOnly? EndDate,
    bool? IsActive,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<InsurancePolicyDto>>;

public class CreateInsurancePolicyCommandHandler
    : IRequestHandler<CreateInsurancePolicyCommand, Result<InsurancePolicyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateInsurancePolicyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<InsurancePolicyDto>> Handle(
        CreateInsurancePolicyCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new InsurancePolicy
        {
            InsurancePolicyId = Guid.NewGuid(),
            PatientId = request.PatientId,
            InsuranceCompanyId = request.InsuranceCompanyId,
            PolicyNumber = request.PolicyNumber,
            CoveragePercentage = request.CoveragePercentage,
            MaxCoverageAmount = request.MaxCoverageAmount,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsActive = request.IsActive,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };

        await _unitOfWork.Repository<InsurancePolicy>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<InsurancePolicyDto>.Success(entity.ToDto());
    }
}
