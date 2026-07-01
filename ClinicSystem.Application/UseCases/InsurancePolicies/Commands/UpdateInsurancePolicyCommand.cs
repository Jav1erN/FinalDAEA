using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsurancePolicies.Commands;

public class UpdateInsurancePolicyCommand : IRequest<Result<InsurancePolicyDto>>
{
    public Guid InsurancePolicyId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid InsuranceCompanyId { get; set; } = Guid.Empty;

    public string PolicyNumber { get; set; } = string.Empty;

    public decimal? CoveragePercentage { get; set; }

    public decimal? MaxCoverageAmount { get; set; }

    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public DateOnly? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

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
        var repository = _unitOfWork.InsurancePolicies;
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

