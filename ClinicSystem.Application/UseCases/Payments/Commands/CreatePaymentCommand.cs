using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Payments.Commands;

public class CreatePaymentCommand : IRequest<Result<PaymentDto>>
{
    public Guid BillingId { get; set; } = Guid.Empty;

    public Guid? InsurancePolicyId { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string? ReferenceNumber { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Status { get; set; }

    public Guid? RegisteredBy { get; set; }
}

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

