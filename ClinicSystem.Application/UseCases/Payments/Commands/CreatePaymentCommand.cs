using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Payments.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Payments.Commands;

public record CreatePaymentCommand(
    Guid BillingId,
    Guid? InsurancePolicyId,
    string PaymentMethod,
    string? ReferenceNumber,
    DateTime? PaymentDate,
    string? Status,
    Guid? RegisteredBy
) : IRequest<Result<PaymentDto>>;

public class CreatePaymentCommandHandler
    : IRequestHandler<CreatePaymentCommand, Result<PaymentDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePaymentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PaymentDto>> Handle(
        CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Payment
        {
            PaymentId = Guid.NewGuid(),
            BillingId = request.BillingId,
            InsurancePolicyId = request.InsurancePolicyId,
            PaymentMethod = request.PaymentMethod,
            ReferenceNumber = request.ReferenceNumber,
            PaymentDate = request.PaymentDate,
            Status = request.Status,
            RegisteredBy = request.RegisteredBy
        };

        await _unitOfWork.Payments.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PaymentDto>.Success(entity.ToDto());
    }
}

