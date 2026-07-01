using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.InsurancePolicies.Commands;

public class CreateInsurancePolicyCommand : IRequest<Result<InsurancePolicyDto>>
{
    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid InsuranceCompanyId { get; set; } = Guid.Empty;

    public string PolicyNumber { get; set; } = string.Empty;

    public decimal? CoveragePercentage { get; set; }

    public decimal? MaxCoverageAmount { get; set; }

    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public DateOnly? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

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

        await _unitOfWork.InsurancePolicies.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<InsurancePolicyDto>.Success(entity.ToDto());
    }
}

