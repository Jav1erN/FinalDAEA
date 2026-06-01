using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Billings.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Billings.Commands;

public record CreateBillingCommand(
    Guid PatientId,
    Guid? AppointmentId,
    Guid? InsurancePolicyId,
    DateTime? IssueDate,
    decimal Subtotal,
    decimal? Discount,
    decimal? InsuranceCoverage,
    string Status,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<BillingDto>>;

public class CreateBillingCommandHandler
    : IRequestHandler<CreateBillingCommand, Result<BillingDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBillingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BillingDto>> Handle(
        CreateBillingCommand request,
        CancellationToken cancellationToken)
    {
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

        await _unitOfWork.Repository<Billing>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<BillingDto>.Success(entity.ToDto());
    }
}
