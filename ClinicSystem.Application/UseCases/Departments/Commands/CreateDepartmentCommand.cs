using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Departments.Commands;

public class CreateDepartmentCommand : IRequest<Result<DepartmentDto>>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}

public class CreateDepartmentCommandHandler
    : IRequestHandler<CreateDepartmentCommand, Result<DepartmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DepartmentDto>> Handle(
        CreateDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Department
        {
            DepartmentId = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive
        };

        await _unitOfWork.Departments.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DepartmentDto>.Success(entity.ToDto());
    }
}

