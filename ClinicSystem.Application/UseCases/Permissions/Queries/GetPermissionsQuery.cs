using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.Permissions.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Permissions.Queries;

public record GetPermissionsQuery : IRequest<Result<IEnumerable<PermissionDto>>>;

public class GetPermissionsQueryHandler
    : IRequestHandler<GetPermissionsQuery, Result<IEnumerable<PermissionDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPermissionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<PermissionDto>>> Handle(
        GetPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<Permission>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<PermissionDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
