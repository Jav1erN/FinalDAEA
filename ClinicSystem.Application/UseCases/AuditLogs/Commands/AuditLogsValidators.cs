using FluentValidation;

namespace ClinicSystem.Application.UseCases.AuditLogs.Commands;

public class CreateAuditLogValidator : AbstractValidator<CreateAuditLogCommand>
{
    public CreateAuditLogValidator()
    {
        RuleFor(x => x.Action).NotEmpty();
        RuleFor(x => x.EntityName).NotEmpty();
    }
}
public class UpdateAuditLogValidator : AbstractValidator<UpdateAuditLogCommand>
{
    public UpdateAuditLogValidator()
    {
        RuleFor(x => x.AuditLogId).NotEmpty();
        RuleFor(x => x.Action).NotEmpty();
        RuleFor(x => x.EntityName).NotEmpty();
    }
}
