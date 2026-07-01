using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Medications.Commands;

public class DeleteMedicationCommand : IRequest<Result<bool>>
{
    public Guid MedicationId { get; set; } = Guid.Empty;
}

public class DeleteMedicationCommandHandler
    : IRequestHandler<DeleteMedicationCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMedicationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteMedicationCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Medications;
        var entity = await repository.GetByIdAsync(request.MedicationId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Medication not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

