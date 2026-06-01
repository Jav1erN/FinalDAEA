using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Departments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Departments.Commands;

public record CreateDepartmentCommand(
    string Name,
    string? Description,
    bool? IsActive
) : IRequest<Result<DepartmentDto>>;

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

        await _unitOfWork.Repository<Department>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<DepartmentDto>.Success(entity.ToDto());
    }
}
