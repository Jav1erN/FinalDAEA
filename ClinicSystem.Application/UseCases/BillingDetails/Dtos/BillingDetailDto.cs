namespace ClinicSystem.Application.UseCases.BillingDetails.Dtos;

public class BillingDetailDto
{
    public Guid BillingDetailId { get; set; } = Guid.Empty;

    public Guid BillingId { get; set; } = Guid.Empty;

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; } = 0;

    public decimal UnitPrice { get; set; } = 0;

    public decimal? Amount { get; set; } = null;
}

