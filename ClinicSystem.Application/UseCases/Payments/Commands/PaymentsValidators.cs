using FluentValidation;

namespace ClinicSystem.Application.UseCases.Payments.Commands;

public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentValidator()
    {
        RuleFor(x => x.BillingId).NotEmpty();
        RuleFor(x => x.PaymentMethod).NotEmpty();
    }
}
public class UpdatePaymentValidator : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentValidator()
    {
        RuleFor(x => x.PaymentId).NotEmpty();
        RuleFor(x => x.BillingId).NotEmpty();
        RuleFor(x => x.PaymentMethod).NotEmpty();
    }
}
