using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.BillingDetails.Dtos;

public static class BillingDetailMappings
{
    public static BillingDetailDto ToDto(this BillingDetail entity)
    {
        return new BillingDetailDto
        {
            BillingDetailId = entity.BillingDetailId,
            BillingId = entity.BillingId,
            Description = entity.Description,
            Quantity = entity.Quantity,
            UnitPrice = entity.UnitPrice,
            Amount = entity.Amount
        };
    }
}
