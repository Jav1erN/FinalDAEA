using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.InsurancePolicies.Dtos;

public static class InsurancePolicyMappings
{
    public static InsurancePolicyDto ToDto(this InsurancePolicy entity)
    {
        return new InsurancePolicyDto
        {
            InsurancePolicyId = entity.InsurancePolicyId,
            PatientId = entity.PatientId,
            InsuranceCompanyId = entity.InsuranceCompanyId,
            PolicyNumber = entity.PolicyNumber,
            CoveragePercentage = entity.CoveragePercentage,
            MaxCoverageAmount = entity.MaxCoverageAmount,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy
        };
    }
}

