using FluentValidation;

namespace ClinicSystem.Application.UseCases.InsurancePolicies.Commands;

public class CreateInsurancePolicyValidator : AbstractValidator<CreateInsurancePolicyCommand>
{
    public CreateInsurancePolicyValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.InsuranceCompanyId).NotEmpty();
        RuleFor(x => x.PolicyNumber).NotEmpty();
        RuleFor(x => x.CoveragePercentage).GreaterThanOrEqualTo(0).When(x => x.CoveragePercentage.HasValue);
        RuleFor(x => x.MaxCoverageAmount).GreaterThanOrEqualTo(0).When(x => x.MaxCoverageAmount.HasValue);
    }
}
public class UpdateInsurancePolicyValidator : AbstractValidator<UpdateInsurancePolicyCommand>
{
    public UpdateInsurancePolicyValidator()
    {
        RuleFor(x => x.InsurancePolicyId).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.InsuranceCompanyId).NotEmpty();
        RuleFor(x => x.PolicyNumber).NotEmpty();
        RuleFor(x => x.CoveragePercentage).GreaterThanOrEqualTo(0).When(x => x.CoveragePercentage.HasValue);
        RuleFor(x => x.MaxCoverageAmount).GreaterThanOrEqualTo(0).When(x => x.MaxCoverageAmount.HasValue);
    }
}
