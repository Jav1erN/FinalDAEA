using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Permissions.Commands;

public class DeletePermissionCommand : IRequest<Result<bool>>
{
    public Guid PermissionId { get; set; } = Guid.Empty;
}

public class DeletePermissionCommandHandler
    : IRequestHandler<DeletePermissionCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePermissionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeletePermissionCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Permissions;
        var entity = await repository.GetByIdAsync(request.PermissionId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Permission not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

