using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Permissions.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Permissions.Queries;

public record GetPermissionByIdQuery(Guid PermissionId) : IRequest<Result<PermissionDto>>;

public class GetPermissionByIdQueryHandler
    : IRequestHandler<GetPermissionByIdQuery, Result<PermissionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPermissionByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PermissionDto>> Handle(
        GetPermissionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<Permission>()
            .GetByIdAsync(request.PermissionId, cancellationToken);

        if (entity is null)
            return Result<PermissionDto>.Failure("Permission not found");

        return Result<PermissionDto>.Success(entity.ToDto());
    }
}
