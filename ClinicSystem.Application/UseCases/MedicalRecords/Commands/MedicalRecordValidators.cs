using FluentValidation;

namespace ClinicSystem.Application.UseCases.MedicalRecords.Commands;

public class CreateMedicalRecordValidator : AbstractValidator<CreateMedicalRecordCommand>
{
    public CreateMedicalRecordValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
    }
}

public class UpdateMedicalRecordValidator : AbstractValidator<UpdateMedicalRecordCommand>
{
    public UpdateMedicalRecordValidator()
    {
        RuleFor(x => x.MedicalRecordId).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
    }
}

