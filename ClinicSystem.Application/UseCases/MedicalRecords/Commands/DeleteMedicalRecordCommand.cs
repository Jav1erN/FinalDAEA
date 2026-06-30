using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.MedicalRecords.Commands;

public record DeleteMedicalRecordCommand(Guid MedicalRecordId) : IRequest<Result<bool>>;

public class DeleteMedicalRecordCommandHandler
    : IRequestHandler<DeleteMedicalRecordCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMedicalRecordCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteMedicalRecordCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.MedicalRecords;
        var entity = await repository.GetByIdAsync(request.MedicalRecordId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("MedicalRecord not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

