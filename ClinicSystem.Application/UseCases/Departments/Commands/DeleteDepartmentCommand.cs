using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Departments.Commands;

public record DeleteDepartmentCommand(Guid DepartmentId) : IRequest<Result<bool>>;

public class DeleteDepartmentCommandHandler
    : IRequestHandler<DeleteDepartmentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Department>();
        var entity = await repository.GetByIdAsync(request.DepartmentId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Department not found");
        
        entity.IsActive = false;
        entity.UpdatedAt = DateTime.Now;

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
