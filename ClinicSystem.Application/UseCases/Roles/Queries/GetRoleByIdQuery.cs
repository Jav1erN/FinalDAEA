using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Roles.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Roles.Queries;

public record GetRoleByIdQuery(Guid RoleId) : IRequest<Result<RoleDto>>;

public class GetRoleByIdQueryHandler
    : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoleDto>> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Role>()
            .GetByIdAsync(request.RoleId, cancellationToken);

        if (entity is null)
            return Result<RoleDto>.Failure("Role not found");

        return Result<RoleDto>.Success(entity.ToDto());
    }
}
