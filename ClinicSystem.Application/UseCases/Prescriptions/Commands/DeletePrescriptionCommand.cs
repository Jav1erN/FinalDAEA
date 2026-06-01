using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Prescriptions.Commands;

public record DeletePrescriptionCommand(Guid PrescriptionId) : IRequest<Result<bool>>;

public class DeletePrescriptionCommandHandler
    : IRequestHandler<DeletePrescriptionCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePrescriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeletePrescriptionCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Prescription>();
        var entity = await repository.GetByIdAsync(request.PrescriptionId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Prescription not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
