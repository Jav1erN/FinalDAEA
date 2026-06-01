using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Roles.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Roles.Commands;

public record CreateRoleCommand(
    string Name,
    string? Description
) : IRequest<Result<RoleDto>>;

public class CreateRoleCommandHandler
    : IRequestHandler<CreateRoleCommand, Result<RoleDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoleDto>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Role
        {
            RoleId = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };

        await _unitOfWork.Repository<Role>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<RoleDto>.Success(entity.ToDto());
    }
}
