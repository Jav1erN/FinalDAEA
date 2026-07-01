using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.AuditLogs.Commands;

public class CreateAuditLogCommand : IRequest<Result<AuditLogDto>>
{
    public Guid? UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public Guid? EntityId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? UserAgent { get; set; }

    public Guid? CorrelationId { get; set; }
}

public class CreateAuditLogCommandHandler
    : IRequestHandler<CreateAuditLogCommand, Result<AuditLogDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuditLogCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuditLogDto>> Handle(
        CreateAuditLogCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new AuditLog
        {
            AuditLogId = Guid.NewGuid(),
            UserId = request.UserId,
            Action = request.Action,
            EntityName = request.EntityName,
            EntityId = request.EntityId,
            OldValues = request.OldValues,
            NewValues = request.NewValues,
            UserAgent = request.UserAgent,
            CorrelationId = request.CorrelationId
        };

        await _unitOfWork.AuditLogs.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AuditLogDto>.Success(entity.ToDto());
    }
}

