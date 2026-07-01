using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.AuditLogs.Commands;

public class DeleteAuditLogCommand : IRequest<Result<bool>>
{
    public Guid AuditLogId { get; set; } = Guid.Empty;
}

public class DeleteAuditLogCommandHandler
    : IRequestHandler<DeleteAuditLogCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuditLogCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteAuditLogCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.AuditLogs;
        var entity = await repository.GetByIdAsync(request.AuditLogId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("AuditLog not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

