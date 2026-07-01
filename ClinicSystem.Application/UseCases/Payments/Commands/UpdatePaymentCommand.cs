using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Payments.Commands;

public class UpdatePaymentCommand : IRequest<Result<PaymentDto>>
{
    public Guid PaymentId { get; set; } = Guid.Empty;

    public Guid BillingId { get; set; } = Guid.Empty;

    public Guid? InsurancePolicyId { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public string? ReferenceNumber { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Status { get; set; }

    public Guid? RegisteredBy { get; set; }
}

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
        var repository = _unitOfWork.Payments;
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

