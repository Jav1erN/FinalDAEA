using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Departments.Commands;

public class UpdateDepartmentCommand : IRequest<Result<DepartmentDto>>
{
    public Guid DepartmentId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

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

