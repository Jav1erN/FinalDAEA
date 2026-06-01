using FluentValidation;

namespace ClinicSystem.Application.UseCases.Patients.Commands;

public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientValidator()
    {
        RuleFor(x => x.DocumentNumber).NotEmpty().MaximumLength(20);
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));
        RuleFor(x => x.Phone).MaximumLength(20);
    }
}
