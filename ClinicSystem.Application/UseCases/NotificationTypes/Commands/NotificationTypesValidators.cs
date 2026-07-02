using FluentValidation;

namespace ClinicSystem.Application.UseCases.NotificationTypes.Commands;

public class CreateNotificationTypeValidator : AbstractValidator<CreateNotificationTypeCommand>
{
    public CreateNotificationTypeValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
public class UpdateNotificationTypeValidator : AbstractValidator<UpdateNotificationTypeCommand>
{
    public UpdateNotificationTypeValidator()
    {
        RuleFor(x => x.TypeId).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
