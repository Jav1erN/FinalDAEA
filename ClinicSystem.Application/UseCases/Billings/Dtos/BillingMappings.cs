using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Billings.Dtos;

public static class BillingMappings
{
    public static BillingDto ToDto(this Billing entity)
    {
        return new BillingDto
        {
            BillingId = entity.BillingId,
            PatientId = entity.PatientId,
            AppointmentId = entity.AppointmentId,
            InsurancePolicyId = entity.InsurancePolicyId,
            IssueDate = entity.IssueDate,
            Subtotal = entity.Subtotal,
            Discount = entity.Discount,
            InsuranceCoverage = entity.InsuranceCoverage,
            TotalAmount = entity.TotalAmount,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy
        };
    }
}

