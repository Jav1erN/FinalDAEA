using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.BillingDetails.Commands;

public class DeleteBillingDetailCommand : IRequest<Result<bool>>
{
    public Guid BillingDetailId { get; set; } = Guid.Empty;
}

public class DeleteBillingDetailCommandHandler
    : IRequestHandler<DeleteBillingDetailCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBillingDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteBillingDetailCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.BillingDetails;
        var entity = await repository.GetByIdAsync(request.BillingDetailId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("BillingDetail not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

