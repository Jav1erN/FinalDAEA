using ClinicSystem.Application.Common.Models;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Payments.Commands;

public class DeletePaymentCommand : IRequest<Result<bool>>
{
    public Guid PaymentId { get; set; } = Guid.Empty;
}

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
        var repository = _unitOfWork.Payments;
        var entity = await repository.GetByIdAsync(request.PaymentId, cancellationToken);

        if (entity is null)
            return Result<bool>.Failure("Payment not found");

        repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}

