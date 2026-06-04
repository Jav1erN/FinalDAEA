using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Departments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Departments.Queries;

public record GetDepartmentsQuery : IRequest<Result<List<DepartmentDto>>>;

public class GetDepartmentsQueryHandler
    : IRequestHandler<GetDepartmentsQuery, Result<List<DepartmentDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDepartmentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<DepartmentDto>>> Handle(
        GetDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<Department>()
            .ListAsync(cancellationToken);

        var result = entities
            .Where(x => x.IsActive == true)
            .Select(x => x.ToDto())
            .ToList();

        return Result<List<DepartmentDto>>.Success(result);
    }
}