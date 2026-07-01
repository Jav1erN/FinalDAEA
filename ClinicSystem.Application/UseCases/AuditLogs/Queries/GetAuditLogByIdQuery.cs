using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
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
        var entity = await _unitOfWork.AuditLogs
            .GetByIdAsync(request.AuditLogId, cancellationToken);

        if (entity is null)
            return Result<AuditLogDto>.Failure("AuditLog not found");

        return Result<AuditLogDto>.Success(entity.ToDto());
    }
}

