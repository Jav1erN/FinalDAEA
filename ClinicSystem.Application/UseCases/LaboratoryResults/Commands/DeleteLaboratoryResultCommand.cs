using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryResults.Commands;

public record DeleteLaboratoryResultCommand(Guid ResultId) : IRequest<Result<bool>>;

public class DeleteLaboratoryResultCommandHandler
    : IRequestHandler<DeleteLaboratoryResultCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLaboratoryResultCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteLaboratoryResultCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<LaboratoryResult>();
        var entity = await repository.GetByIdAsync(request.ResultId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("LaboratoryResult not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
