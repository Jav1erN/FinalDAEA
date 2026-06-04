using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Billings.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Billings.Commands;

public record UpdateBillingCommand(
    Guid BillingId,
    Guid PatientId,
    Guid? AppointmentId,
    Guid? InsurancePolicyId,
    DateTime? IssueDate,
    decimal Subtotal,
    decimal? Discount,
    decimal? InsuranceCoverage,
    string Status,
    DateTime? UpdatedAt,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<BillingDto>>;

public class UpdateBillingCommandHandler
    : IRequestHandler<UpdateBillingCommand, Result<BillingDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBillingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BillingDto>> Handle(
        UpdateBillingCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<Billing>();
        var entity = await repository.GetByIdAsync(request.BillingId, cancellationToken);

        if (entity is null)
            return Result<BillingDto>.Failure("Billing not found");

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
