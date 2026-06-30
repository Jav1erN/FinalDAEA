using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Roles.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Roles.Queries;

public record GetRolesQuery : IRequest<Result<IEnumerable<RoleDto>>>;

public class GetRolesQueryHandler
    : IRequestHandler<GetRolesQuery, Result<IEnumerable<RoleDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRolesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<RoleDto>>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Roles
            .ListAsync(cancellationToken);

        return Result<IEnumerable<RoleDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}

