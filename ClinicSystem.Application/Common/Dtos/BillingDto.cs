namespace ClinicSystem.Application.Common.Dtos;

public class BillingDto
{
    public Guid BillingId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid? AppointmentId { get; set; } = null;

    public Guid? InsurancePolicyId { get; set; } = null;

    public DateTime? IssueDate { get; set; } = null;

    public decimal Subtotal { get; set; } = 0;

    public decimal? Discount { get; set; } = null;

    public decimal? InsuranceCoverage { get; set; } = null;

    public decimal? TotalAmount { get; set; } = null;

    public string Status { get; set; } = string.Empty;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime? DeletedAt { get; set; } = null;

    public Guid? CreatedBy { get; set; } = null;

    public Guid? UpdatedBy { get; set; } = null;
}

