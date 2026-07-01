using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using ClinicSystem.Domain.Ports.Services;
using MediatR;

namespace ClinicSystem.Application.UseCases.Billings.Commands;

public class CreateBillingCommand : IRequest<Result<BillingDto>>
{
    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid? AppointmentId { get; set; }

    public Guid? InsurancePolicyId { get; set; }

    public DateTime? IssueDate { get; set; }

    public decimal Subtotal { get; set; }

    public decimal? Discount { get; set; }

    public decimal? InsuranceCoverage { get; set; }

    public string Status { get; set; } = string.Empty;

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

public class CreateBillingCommandHandler
    : IRequestHandler<CreateBillingCommand, Result<BillingDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillingPolicyService _billingPolicyService;

    public CreateBillingCommandHandler(
        IUnitOfWork unitOfWork,
        IBillingPolicyService billingPolicyService)
    {
        _unitOfWork = unitOfWork;
        _billingPolicyService = billingPolicyService;
    }

    public async Task<Result<BillingDto>> Handle(
        CreateBillingCommand request,
        CancellationToken cancellationToken)
    {
        var billingResult = _billingPolicyService.ValidateAmounts(
            request.Subtotal,
            request.Discount,
            request.InsuranceCoverage);

        if (!billingResult.IsValid)
            return Result<BillingDto>.Failure(billingResult.Error!);

        var entity = new Billing
        {
            BillingId = Guid.NewGuid(),
            PatientId = request.PatientId,
            AppointmentId = request.AppointmentId,
            InsurancePolicyId = request.InsurancePolicyId,
            IssueDate = request.IssueDate,
            Subtotal = request.Subtotal,
            Discount = request.Discount,
            InsuranceCoverage = request.InsuranceCoverage,
            Status = request.Status,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };

        await _unitOfWork.Billings.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<BillingDto>.Success(entity.ToDto());
    }
}

