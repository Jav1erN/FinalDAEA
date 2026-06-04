using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.NotificationTypes.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.NotificationTypes.Queries;

public record GetNotificationTypesQuery : IRequest<Result<IEnumerable<NotificationTypeDto>>>;

public class GetNotificationTypesQueryHandler
    : IRequestHandler<GetNotificationTypesQuery, Result<IEnumerable<NotificationTypeDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetNotificationTypesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<NotificationTypeDto>>> Handle(
        GetNotificationTypesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<NotificationType>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<NotificationTypeDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
