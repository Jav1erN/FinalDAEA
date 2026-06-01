using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.VitalSigns.Commands;

public record DeleteVitalSignCommand(Guid VitalSignId) : IRequest<Result<bool>>;

public class DeleteVitalSignCommandHandler
    : IRequestHandler<DeleteVitalSignCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVitalSignCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteVitalSignCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<VitalSign>();
        var entity = await repository.GetByIdAsync(request.VitalSignId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("VitalSign not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
