using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Diagnoses.Commands;

public record DeleteDiagnosisCommand(Guid DiagnosisId) : IRequest<Result<bool>>;

public class DeleteDiagnosisCommandHandler
    : IRequestHandler<DeleteDiagnosisCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDiagnosisCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteDiagnosisCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Diagnosis>();
        var entity = await repository.GetByIdAsync(request.DiagnosisId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Diagnosis not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
