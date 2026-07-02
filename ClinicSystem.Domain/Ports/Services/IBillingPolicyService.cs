using ClinicSystem.Domain.Common;

namespace ClinicSystem.Domain.Ports.Services;

public interface IBillingPolicyService
{
    BusinessRuleResult ValidateAmounts(
        decimal subtotal,
        decimal? discount,
        decimal? insuranceCoverage);

    decimal CalculateTotal(
        decimal subtotal,
        decimal? discount,
        decimal? insuranceCoverage);
}
