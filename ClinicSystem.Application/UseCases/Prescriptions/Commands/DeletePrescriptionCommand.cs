using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Prescriptions.Commands;

public class DeletePrescriptionCommand : IRequest<Result<bool>>
{
    public Guid PrescriptionId { get; set; } = Guid.Empty;
}

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
        var repository = _unitOfWork.Prescriptions;
        var entity = await repository.GetByIdAsync(request.PrescriptionId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Prescription not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

