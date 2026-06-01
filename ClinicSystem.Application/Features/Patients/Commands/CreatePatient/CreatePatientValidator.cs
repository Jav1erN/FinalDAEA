using FluentValidation;

namespace ClinicSystem.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}