using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Notifications.Commands;

public class UpdateNotificationCommand : IRequest<Result<NotificationDto>>
{
    public Guid NotificationId { get; set; } = Guid.Empty;

    public Guid UserId { get; set; } = Guid.Empty;

    public Guid TypeId { get; set; } = Guid.Empty;

    public string Channel { get; set; } = string.Empty;

    public string? Status { get; set; }

    public string? EntityType { get; set; }

    public Guid? EntityId { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public DateTime? ScheduledAt { get; set; }

    public DateTime? SentAt { get; set; }

    public DateTime? ReadAt { get; set; }
}

public class UpdateNotificationCommandHandler
    : IRequestHandler<UpdateNotificationCommand, Result<NotificationDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNotificationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<NotificationDto>> Handle(
        UpdateNotificationCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Notifications;
        var entity = await repository.GetByIdAsync(request.NotificationId, cancellationToken);

        if (entity is null)
            return Result<NotificationDto>.Failure("Notification not found");

        entity.UserId = request.UserId;
        entity.TypeId = request.TypeId;
        entity.Channel = request.Channel;
        entity.Status = request.Status;
        entity.EntityType = request.EntityType;
        entity.EntityId = request.EntityId;
        entity.Subject = request.Subject;
        entity.Body = request.Body;
        entity.ScheduledAt = request.ScheduledAt;
        entity.SentAt = request.SentAt;
        entity.ReadAt = request.ReadAt;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<NotificationDto>.Success(entity.ToDto());
    }
}

