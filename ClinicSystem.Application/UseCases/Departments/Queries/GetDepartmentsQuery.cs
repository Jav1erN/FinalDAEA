using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Departments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Departments.Queries;

public record GetDepartmentsQuery : IRequest<Result<IEnumerable<DepartmentDto>>>;

public class GetDepartmentsQueryHandler
    : IRequestHandler<GetDepartmentsQuery, Result<IEnumerable<DepartmentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDepartmentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<DepartmentDto>>> Handle(
        GetDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<Department>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<DepartmentDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
