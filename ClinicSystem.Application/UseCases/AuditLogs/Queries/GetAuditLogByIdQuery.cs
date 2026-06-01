using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.AuditLogs.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.AuditLogs.Queries;

public record GetAuditLogByIdQuery(Guid AuditLogId) : IRequest<Result<AuditLogDto>>;

public class GetAuditLogByIdQueryHandler
    : IRequestHandler<GetAuditLogByIdQuery, Result<AuditLogDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuditLogByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuditLogDto>> Handle(
        GetAuditLogByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<AuditLog>()
            .GetByIdAsync(request.AuditLogId, cancellationToken);

        if (entity is null)
            return Result<AuditLogDto>.Failure("AuditLog not found");

        return Result<AuditLogDto>.Success(entity.ToDto());
    }
}
