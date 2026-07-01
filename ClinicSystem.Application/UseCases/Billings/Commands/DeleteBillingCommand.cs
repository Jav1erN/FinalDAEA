using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Billings.Commands;

public class DeleteBillingCommand : IRequest<Result<bool>>
{
    public Guid BillingId { get; set; } = Guid.Empty;
}

public class DeleteBillingCommandHandler
    : IRequestHandler<DeleteBillingCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBillingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteBillingCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Billings;
        var entity = await repository.GetByIdAsync(request.BillingId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Billing not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

