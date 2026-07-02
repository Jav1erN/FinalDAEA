using FluentValidation;

namespace ClinicSystem.Application.UseCases.AppointmentStatuses.Commands;

public class CreateAppointmentStatusValidator : AbstractValidator<CreateAppointmentStatusCommand>
{
    public CreateAppointmentStatusValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
public class UpdateAppointmentStatusValidator : AbstractValidator<UpdateAppointmentStatusCommand>
{
    public UpdateAppointmentStatusValidator()
    {
        RuleFor(x => x.StatusId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
