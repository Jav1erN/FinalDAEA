using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Roles.Commands;

public class CreateRoleCommand : IRequest<Result<RoleDto>>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}

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

        await _unitOfWork.Roles.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<RoleDto>.Success(entity.ToDto());
    }
}

