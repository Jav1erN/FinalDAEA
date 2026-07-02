using FluentValidation;

namespace ClinicSystem.Application.UseCases.BillingDetails.Commands;

public class CreateBillingDetailValidator : AbstractValidator<CreateBillingDetailCommand>
{
    public CreateBillingDetailValidator()
    {
        RuleFor(x => x.BillingId).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
public class UpdateBillingDetailValidator : AbstractValidator<UpdateBillingDetailCommand>
{
    public UpdateBillingDetailValidator()
    {
        RuleFor(x => x.BillingDetailId).NotEmpty();
        RuleFor(x => x.BillingId).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
