using FluentValidation;

namespace ClinicSystem.Application.UseCases.Medications.Commands;

public class CreateMedicationValidator : AbstractValidator<CreateMedicationCommand>
{
    public CreateMedicationValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).When(x => x.Stock.HasValue);
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0).When(x => x.UnitPrice.HasValue);
    }
}
public class UpdateMedicationValidator : AbstractValidator<UpdateMedicationCommand>
{
    public UpdateMedicationValidator()
    {
        RuleFor(x => x.MedicationId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).When(x => x.Stock.HasValue);
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0).When(x => x.UnitPrice.HasValue);
    }
}
