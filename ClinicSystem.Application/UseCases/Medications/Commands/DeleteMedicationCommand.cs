using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Medications.Commands;

public record DeleteMedicationCommand(Guid MedicationId) : IRequest<Result<bool>>;

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
        var entity = await _unitOfWork.Repository<Medication>()
            .GetByIdAsync(request.MedicationId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Medication not found");

        entity.DeletedAt = DateTime.Now;
        entity.IsActive = false;

        _unitOfWork.Repository<Medication>().Update(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}