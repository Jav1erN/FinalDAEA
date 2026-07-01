using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.AuditLogs.Commands;

public class UpdateAuditLogCommand : IRequest<Result<AuditLogDto>>
{
    public Guid AuditLogId { get; set; } = Guid.Empty;

    public Guid? UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public Guid? EntityId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? UserAgent { get; set; }

    public Guid? CorrelationId { get; set; }
}

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

