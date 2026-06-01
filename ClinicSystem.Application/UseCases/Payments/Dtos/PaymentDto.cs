namespace ClinicSystem.Application.UseCases.Payments.Dtos;

public class PaymentDto
{
    public Guid PaymentId { get; set; } = Guid.Empty;

    public Guid BillingId { get; set; } = Guid.Empty;

    public Guid? InsurancePolicyId { get; set; } = null;

    public decimal Amount { get; set; } = 0;

    public string PaymentMethod { get; set; } = string.Empty;

    public string? ReferenceNumber { get; set; } = null;

    public DateTime? PaymentDate { get; set; } = null;

    public string? Status { get; set; } = null;

    public Guid? RegisteredBy { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;
}
