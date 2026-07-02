using FluentValidation;

namespace ClinicSystem.Application.UseCases.Billings.Commands;

public class CreateBillingValidator : AbstractValidator<CreateBillingCommand>
{
    public CreateBillingValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).When(x => x.Discount.HasValue);
        RuleFor(x => x.InsuranceCoverage).GreaterThanOrEqualTo(0).When(x => x.InsuranceCoverage.HasValue);
        RuleFor(x => x.Status).NotEmpty();
    }
}
public class UpdateBillingValidator : AbstractValidator<UpdateBillingCommand>
{
    public UpdateBillingValidator()
    {
        RuleFor(x => x.BillingId).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).When(x => x.Discount.HasValue);
        RuleFor(x => x.InsuranceCoverage).GreaterThanOrEqualTo(0).When(x => x.InsuranceCoverage.HasValue);
        RuleFor(x => x.Status).NotEmpty();
    }
}
