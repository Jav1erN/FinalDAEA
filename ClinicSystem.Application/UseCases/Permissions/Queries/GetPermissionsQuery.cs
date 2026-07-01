using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
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
        var entities = await _unitOfWork.Permissions
            .ListAsync(cancellationToken);

        return Result<IEnumerable<PermissionDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

