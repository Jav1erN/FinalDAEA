using FluentValidation;

namespace ClinicSystem.Application.UseCases.InsuranceCompanies.Commands;

public class CreateInsuranceCompanyValidator : AbstractValidator<CreateInsuranceCompanyCommand>
{
    public CreateInsuranceCompanyValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
public class UpdateInsuranceCompanyValidator : AbstractValidator<UpdateInsuranceCompanyCommand>
{
    public UpdateInsuranceCompanyValidator()
    {
        RuleFor(x => x.InsuranceCompanyId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
