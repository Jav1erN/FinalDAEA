using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.InsurancePolicies.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsurancePolicies.Commands;

public record UpdateInsurancePolicyCommand(
    Guid InsurancePolicyId,
    Guid PatientId,
    Guid InsuranceCompanyId,
    string PolicyNumber,
    decimal? CoveragePercentage,
    decimal? MaxCoverageAmount,
    DateOnly StartDate,
    DateOnly? EndDate,
    bool? IsActive,
    DateTime? UpdatedAt,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<InsurancePolicyDto>>;

public class UpdateInsurancePolicyCommandHandler
    : IRequestHandler<UpdateInsurancePolicyCommand, Result<InsurancePolicyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInsurancePolicyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<InsurancePolicyDto>> Handle(
        UpdateInsurancePolicyCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<InsurancePolicy>();
        var entity = await repository.GetByIdAsync(request.InsurancePolicyId, cancellationToken);

        if (entity is null)
            return Result<InsurancePolicyDto>.Failure("InsurancePolicy not found");

        entity.PatientId = request.PatientId;
        entity.InsuranceCompanyId = request.InsuranceCompanyId;
        entity.PolicyNumber = request.PolicyNumber;
        entity.CoveragePercentage = request.CoveragePercentage;
        entity.MaxCoverageAmount = request.MaxCoverageAmount;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = request.UpdatedAt;
        entity.CreatedBy = request.CreatedBy;
        entity.UpdatedBy = request.UpdatedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<InsurancePolicyDto>.Success(entity.ToDto());
    }
}
