using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.PrescriptionDetails.Commands;

public record DeletePrescriptionDetailCommand(Guid PrescriptionDetailId) : IRequest<Result<bool>>;

public class DeletePrescriptionDetailCommandHandler
    : IRequestHandler<DeletePrescriptionDetailCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePrescriptionDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeletePrescriptionDetailCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<PrescriptionDetail>();
        var entity = await repository.GetByIdAsync(request.PrescriptionDetailId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("PrescriptionDetail not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
