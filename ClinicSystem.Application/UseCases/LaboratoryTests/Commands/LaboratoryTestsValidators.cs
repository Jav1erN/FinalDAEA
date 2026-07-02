using FluentValidation;

namespace ClinicSystem.Application.UseCases.LaboratoryTests.Commands;

public class CreateLaboratoryTestValidator : AbstractValidator<CreateLaboratoryTestCommand>
{
    public CreateLaboratoryTestValidator()
    {
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.TestName).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
    }
}
public class UpdateLaboratoryTestValidator : AbstractValidator<UpdateLaboratoryTestCommand>
{
    public UpdateLaboratoryTestValidator()
    {
        RuleFor(x => x.LaboratoryTestId).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.TestName).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
    }
}
