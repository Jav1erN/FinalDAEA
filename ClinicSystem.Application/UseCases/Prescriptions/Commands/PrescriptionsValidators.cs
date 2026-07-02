using FluentValidation;

namespace ClinicSystem.Application.UseCases.Prescriptions.Commands;

public class CreatePrescriptionValidator : AbstractValidator<CreatePrescriptionCommand>
{
    public CreatePrescriptionValidator()
    {
        RuleFor(x => x.MedicalRecordId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();
    }
}
public class UpdatePrescriptionValidator : AbstractValidator<UpdatePrescriptionCommand>
{
    public UpdatePrescriptionValidator()
    {
        RuleFor(x => x.PrescriptionId).NotEmpty();
        RuleFor(x => x.MedicalRecordId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();
    }
}
