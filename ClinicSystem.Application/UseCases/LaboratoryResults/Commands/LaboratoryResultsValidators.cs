using FluentValidation;

namespace ClinicSystem.Application.UseCases.LaboratoryResults.Commands;

public class CreateLaboratoryResultValidator : AbstractValidator<CreateLaboratoryResultCommand>
{
    public CreateLaboratoryResultValidator()
    {
        RuleFor(x => x.LaboratoryTestId).NotEmpty();
        RuleFor(x => x.ParameterName).NotEmpty();
    }
}
public class UpdateLaboratoryResultValidator : AbstractValidator<UpdateLaboratoryResultCommand>
{
    public UpdateLaboratoryResultValidator()
    {
        RuleFor(x => x.ResultId).NotEmpty();
        RuleFor(x => x.LaboratoryTestId).NotEmpty();
        RuleFor(x => x.ParameterName).NotEmpty();
    }
}
