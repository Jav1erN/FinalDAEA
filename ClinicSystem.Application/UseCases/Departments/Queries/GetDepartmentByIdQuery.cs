using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Departments.Queries;

public record GetDepartmentByIdQuery(Guid DepartmentId) : IRequest<Result<DepartmentDto>>;

public class GetDepartmentByIdQueryHandler
    : IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDepartmentByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DepartmentDto>> Handle(
        GetDepartmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Departments
            .GetByIdAsync(request.DepartmentId, cancellationToken);

        if (entity is null)
            return Result<DepartmentDto>.Failure("Department not found");

        return Result<DepartmentDto>.Success(entity.ToDto());
    }
}

