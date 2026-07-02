using FluentValidation;

namespace ClinicSystem.Application.UseCases.Treatments.Commands;

public class CreateTreatmentValidator : AbstractValidator<CreateTreatmentCommand>
{
    public CreateTreatmentValidator()
    {
        RuleFor(x => x.MedicalRecordId).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
public class UpdateTreatmentValidator : AbstractValidator<UpdateTreatmentCommand>
{
    public UpdateTreatmentValidator()
    {
        RuleFor(x => x.TreatmentId).NotEmpty();
        RuleFor(x => x.MedicalRecordId).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
