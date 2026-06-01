using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.NotificationTypes.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.NotificationTypes.Queries;

public record GetNotificationTypeByIdQuery(Guid TypeId) : IRequest<Result<NotificationTypeDto>>;

public class GetNotificationTypeByIdQueryHandler
    : IRequestHandler<GetNotificationTypeByIdQuery, Result<NotificationTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetNotificationTypeByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<NotificationTypeDto>> Handle(
        GetNotificationTypeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<NotificationType>()
            .GetByIdAsync(request.TypeId, cancellationToken);

        if (entity is null)
            return Result<NotificationTypeDto>.Failure("NotificationType not found");

        return Result<NotificationTypeDto>.Success(entity.ToDto());
    }
}
