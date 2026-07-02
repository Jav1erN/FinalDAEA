using FluentValidation;

namespace ClinicSystem.Application.UseCases.Diagnoses.Commands;

public class CreateDiagnosisValidator : AbstractValidator<CreateDiagnosisCommand>
{
    public CreateDiagnosisValidator()
    {
        RuleFor(x => x.MedicalRecordId).NotEmpty();
        RuleFor(x => x.Cie10Code).NotEmpty().MaximumLength(10);
    }
}

public class UpdateDiagnosisValidator : AbstractValidator<UpdateDiagnosisCommand>
{
    public UpdateDiagnosisValidator()
    {
        RuleFor(x => x.DiagnosisId).NotEmpty();
        RuleFor(x => x.MedicalRecordId).NotEmpty();
        RuleFor(x => x.Cie10Code).NotEmpty().MaximumLength(10);
    }
}

