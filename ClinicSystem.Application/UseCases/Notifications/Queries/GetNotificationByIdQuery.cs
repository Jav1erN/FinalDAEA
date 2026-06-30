using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Notifications.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Notifications.Queries;

public record GetNotificationByIdQuery(Guid NotificationId) : IRequest<Result<NotificationDto>>;

public class GetNotificationByIdQueryHandler
    : IRequestHandler<GetNotificationByIdQuery, Result<NotificationDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetNotificationByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<NotificationDto>> Handle(
        GetNotificationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Notifications
            .GetByIdAsync(request.NotificationId, cancellationToken);

        if (entity is null)
            return Result<NotificationDto>.Failure("Notification not found");

        return Result<NotificationDto>.Success(entity.ToDto());
    }
}

