using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Departments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Departments.Commands;

public record UpdateDepartmentCommand(
    Guid DepartmentId,
    string Name,
    string? Description,
    bool? IsActive,
    DateTime? UpdatedAt
) : IRequest<Result<DepartmentDto>>;

public class UpdateDepartmentCommandHandler
    : IRequestHandler<UpdateDepartmentCommand, Result<DepartmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DepartmentDto>> Handle(
        UpdateDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Departments;
        var entity = await repository.GetByIdAsync(request.DepartmentId, cancellationToken);

        if (entity is null)
            return Result<DepartmentDto>.Failure("Department not found");

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = request.UpdatedAt;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DepartmentDto>.Success(entity.ToDto());
    }
}

