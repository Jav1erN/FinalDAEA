using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.LaboratoryResults.Commands;

public class DeleteLaboratoryResultCommand : IRequest<Result<bool>>
{
    public Guid ResultId { get; set; } = Guid.Empty;
}

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
        var repository = _unitOfWork.LaboratoryResults;
        var entity = await repository.GetByIdAsync(request.ResultId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("LaboratoryResult not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

