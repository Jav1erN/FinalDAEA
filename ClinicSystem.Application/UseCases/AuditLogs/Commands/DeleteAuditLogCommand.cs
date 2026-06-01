using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.AuditLogs.Commands;

public record DeleteAuditLogCommand(Guid AuditLogId) : IRequest<Result<bool>>;

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
        var repository = _unitOfWork.Repository<AuditLog>();
        var entity = await repository.GetByIdAsync(request.AuditLogId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("AuditLog not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
