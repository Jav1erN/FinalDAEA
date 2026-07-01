using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Treatments.Commands;

public class DeleteTreatmentCommand : IRequest<Result<bool>>
{
    public Guid TreatmentId { get; set; } = Guid.Empty;
}

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
        var repository = _unitOfWork.Treatments;
        var entity = await repository.GetByIdAsync(request.TreatmentId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Treatment not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

