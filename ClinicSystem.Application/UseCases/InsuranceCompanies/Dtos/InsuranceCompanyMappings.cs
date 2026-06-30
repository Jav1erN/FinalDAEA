using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.InsuranceCompanies.Dtos;

public static class InsuranceCompanyMappings
{
    public static InsuranceCompanyDto ToDto(this InsuranceCompany entity)
    {
        return new InsuranceCompanyDto
        {
            InsuranceCompanyId = entity.InsuranceCompanyId,
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
            ContactName = entity.ContactName,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}

