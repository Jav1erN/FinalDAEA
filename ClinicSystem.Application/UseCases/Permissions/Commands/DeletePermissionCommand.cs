using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Permissions.Commands;

public record DeletePermissionCommand(Guid PermissionId) : IRequest<Result<bool>>;

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
        var repository = _unitOfWork.Repository<Permission>();
        var entity = await repository.GetByIdAsync(request.PermissionId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Permission not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
