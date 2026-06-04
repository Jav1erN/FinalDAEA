using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.AuditLogs.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.AuditLogs.Queries;

public record GetAuditLogsQuery : IRequest<Result<IEnumerable<AuditLogDto>>>;

public class GetAuditLogsQueryHandler
    : IRequestHandler<GetAuditLogsQuery, Result<IEnumerable<AuditLogDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuditLogsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<AuditLogDto>>> Handle(
        GetAuditLogsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<AuditLog>()
            .ListAsync(cancellationToken);

        return Result<IEnumerable<AuditLogDto>>.Success(entities.Select(entity => entity.ToDto()));
    }
}
