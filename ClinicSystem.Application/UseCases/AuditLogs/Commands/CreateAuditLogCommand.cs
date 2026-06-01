using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.AuditLogs.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.AuditLogs.Commands;

public record CreateAuditLogCommand(
    Guid? UserId,
    string Action,
    string EntityName,
    Guid? EntityId,
    string? OldValues,
    string? NewValues,
    string? UserAgent,
    Guid? CorrelationId
) : IRequest<Result<AuditLogDto>>;

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

        await _unitOfWork.Repository<AuditLog>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AuditLogDto>.Success(entity.ToDto());
    }
}
