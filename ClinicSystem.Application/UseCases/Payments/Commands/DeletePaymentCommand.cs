using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Payments.Commands;

public record DeletePaymentCommand(Guid PaymentId) : IRequest<Result<bool>>;

public class DeletePaymentCommandHandler
    : IRequestHandler<DeletePaymentCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePaymentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeletePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Payment>();
        var entity = await repository.GetByIdAsync(request.PaymentId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Payment not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
