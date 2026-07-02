using FluentValidation;

namespace ClinicSystem.Application.UseCases.Specialties.Commands;

public class CreateSpecialtyValidator : AbstractValidator<CreateSpecialtyCommand>
{
    public CreateSpecialtyValidator()
    {
        RuleFor(x => x.DepartmentId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
public class UpdateSpecialtyValidator : AbstractValidator<UpdateSpecialtyCommand>
{
    public UpdateSpecialtyValidator()
    {
        RuleFor(x => x.SpecialtyId).NotEmpty();
        RuleFor(x => x.DepartmentId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
