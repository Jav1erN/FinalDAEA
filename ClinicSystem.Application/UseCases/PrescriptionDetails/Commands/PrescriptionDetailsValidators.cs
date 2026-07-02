using FluentValidation;

namespace ClinicSystem.Application.UseCases.PrescriptionDetails.Commands;

public class CreatePrescriptionDetailValidator : AbstractValidator<CreatePrescriptionDetailCommand>
{
    public CreatePrescriptionDetailValidator()
    {
        RuleFor(x => x.PrescriptionId).NotEmpty();
        RuleFor(x => x.MedicationId).NotEmpty();
        RuleFor(x => x.DurationDays).GreaterThanOrEqualTo(0).When(x => x.DurationDays.HasValue);
    }
}
public class UpdatePrescriptionDetailValidator : AbstractValidator<UpdatePrescriptionDetailCommand>
{
    public UpdatePrescriptionDetailValidator()
    {
        RuleFor(x => x.PrescriptionDetailId).NotEmpty();
        RuleFor(x => x.PrescriptionId).NotEmpty();
        RuleFor(x => x.MedicationId).NotEmpty();
        RuleFor(x => x.DurationDays).GreaterThanOrEqualTo(0).When(x => x.DurationDays.HasValue);
    }
}
