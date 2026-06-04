using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Treatments.Commands;

public record DeleteTreatmentCommand(Guid TreatmentId) : IRequest<Result<bool>>;

public class DeleteTreatmentCommandHandler
    : IRequestHandler<DeleteTreatmentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTreatmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteTreatmentCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Treatment>();
        var entity = await repository.GetByIdAsync(request.TreatmentId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Treatment not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
