using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.AuditLogs.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.AuditLogs.Commands;

public record UpdateAuditLogCommand(
    Guid AuditLogId,
    Guid? UserId,
    string Action,
    string EntityName,
    Guid? EntityId,
    string? OldValues,
    string? NewValues,
    string? UserAgent,
    Guid? CorrelationId
) : IRequest<Result<AuditLogDto>>;

public class UpdateAuditLogCommandHandler
    : IRequestHandler<UpdateAuditLogCommand, Result<AuditLogDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuditLogCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuditLogDto>> Handle(
        UpdateAuditLogCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.AuditLogs;
        var entity = await repository.GetByIdAsync(request.AuditLogId, cancellationToken);

        if (entity is null)
            return Result<AuditLogDto>.Failure("AuditLog not found");

        entity.UserId = request.UserId;
        entity.Action = request.Action;
        entity.EntityName = request.EntityName;
        entity.EntityId = request.EntityId;
        entity.OldValues = request.OldValues;
        entity.NewValues = request.NewValues;
        entity.UserAgent = request.UserAgent;
        entity.CorrelationId = request.CorrelationId;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AuditLogDto>.Success(entity.ToDto());
    }
}

