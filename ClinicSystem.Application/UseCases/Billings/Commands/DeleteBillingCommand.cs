using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.Billings.Commands;

public record DeleteBillingCommand(Guid BillingId) : IRequest<Result<bool>>;

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
        var repository = _unitOfWork.Repository<Billing>();
        var entity = await repository.GetByIdAsync(request.BillingId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Billing not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
