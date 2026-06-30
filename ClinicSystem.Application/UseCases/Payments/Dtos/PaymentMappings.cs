using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Payments.Dtos;

public static class PaymentMappings
{
    public static PaymentDto ToDto(this Payment entity)
    {
        return new PaymentDto
        {
            PaymentId = entity.PaymentId,
            BillingId = entity.BillingId,
            InsurancePolicyId = entity.InsurancePolicyId,
            Amount = entity.Amount,
            PaymentMethod = entity.PaymentMethod,
            ReferenceNumber = entity.ReferenceNumber,
            PaymentDate = entity.PaymentDate,
            Status = entity.Status,
            RegisteredBy = entity.RegisteredBy,
            CreatedAt = entity.CreatedAt
        };
    }
}

