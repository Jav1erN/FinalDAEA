using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Domain.Ports.Services;
using MediatR;

namespace ClinicSystem.Application.UseCases.Billings.Commands;

public class UpdateBillingCommand : IRequest<Result<BillingDto>>
{
    public Guid BillingId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid? AppointmentId { get; set; }

    public Guid? InsurancePolicyId { get; set; }

    public DateTime? IssueDate { get; set; }

    public decimal Subtotal { get; set; }

    public decimal? Discount { get; set; }

    public decimal? InsuranceCoverage { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

public class UpdateBillingCommandHandler
    : IRequestHandler<UpdateBillingCommand, Result<BillingDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillingPolicyService _billingPolicyService;

    public UpdateBillingCommandHandler(
        IUnitOfWork unitOfWork,
        IBillingPolicyService billingPolicyService)
    {
        _unitOfWork = unitOfWork;
        _billingPolicyService = billingPolicyService;
    }

    public async Task<Result<BillingDto>> Handle(
        UpdateBillingCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Billings;
        var entity = await repository.GetByIdAsync(request.BillingId, cancellationToken);

        if (entity is null)
            return Result<BillingDto>.Failure("Billing not found");

        var billingResult = _billingPolicyService.ValidateAmounts(
            request.Subtotal,
            request.Discount,
            request.InsuranceCoverage);

        if (!billingResult.IsValid)
            return Result<BillingDto>.Failure(billingResult.Error!);

        entity.PatientId = request.PatientId;
        entity.AppointmentId = request.AppointmentId;
        entity.InsurancePolicyId = request.InsurancePolicyId;
        entity.IssueDate = request.IssueDate;
        entity.Subtotal = request.Subtotal;
        entity.Discount = request.Discount;
        entity.InsuranceCoverage = request.InsuranceCoverage;
        entity.Status = request.Status;
        entity.UpdatedAt = request.UpdatedAt;
        entity.CreatedBy = request.CreatedBy;
        entity.UpdatedBy = request.UpdatedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<BillingDto>.Success(entity.ToDto());
    }
}

