using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Payments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Payments.Commands;

public record UpdatePaymentCommand(
    Guid PaymentId,
    Guid BillingId,
    Guid? InsurancePolicyId,
    string PaymentMethod,
    string? ReferenceNumber,
    DateTime? PaymentDate,
    string? Status,
    Guid? RegisteredBy
) : IRequest<Result<PaymentDto>>;

public class UpdatePaymentCommandHandler
    : IRequestHandler<UpdatePaymentCommand, Result<PaymentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PaymentDto>> Handle(
        UpdatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Payment>();
        var entity = await repository.GetByIdAsync(request.PaymentId, cancellationToken);

        if (entity is null)
            return Result<PaymentDto>.Failure("Payment not found");

        entity.BillingId = request.BillingId;
        entity.InsurancePolicyId = request.InsurancePolicyId;
        entity.PaymentMethod = request.PaymentMethod;
        entity.ReferenceNumber = request.ReferenceNumber;
        entity.PaymentDate = request.PaymentDate;
        entity.Status = request.Status;
        entity.RegisteredBy = request.RegisteredBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PaymentDto>.Success(entity.ToDto());
    }
}
