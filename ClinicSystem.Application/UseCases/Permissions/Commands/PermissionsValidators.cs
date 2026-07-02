using FluentValidation;

namespace ClinicSystem.Application.UseCases.Permissions.Commands;

public class CreatePermissionValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionValidator()
    {
        RuleFor(x => x.Resource).NotEmpty();
        RuleFor(x => x.Action).NotEmpty();
    }
}
public class UpdatePermissionValidator : AbstractValidator<UpdatePermissionCommand>
{
    public UpdatePermissionValidator()
    {
        RuleFor(x => x.PermissionId).NotEmpty();
        RuleFor(x => x.Resource).NotEmpty();
        RuleFor(x => x.Action).NotEmpty();
    }
}
