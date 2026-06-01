using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Notifications.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Notifications.Queries;

public record GetNotificationsQuery : IRequest<Result<IEnumerable<NotificationDto>>>;

public class GetNotificationsQueryHandler
    : IRequestHandler<GetNotificationsQuery, Result<IEnumerable<NotificationDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetNotificationsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<NotificationDto>>> Handle(
        GetNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<Notification>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<NotificationDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
