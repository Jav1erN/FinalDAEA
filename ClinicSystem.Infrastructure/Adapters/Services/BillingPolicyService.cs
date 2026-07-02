using ClinicSystem.Domain.Common;
using ClinicSystem.Domain.Ports.Services;

namespace ClinicSystem.Infrastructure.Adapters.Services;

public class BillingPolicyService : IBillingPolicyService
{
    public BusinessRuleResult ValidateAmounts(
        decimal subtotal,
        decimal? discount,
        decimal? insuranceCoverage)
    {
        if (subtotal < 0)
            return BusinessRuleResult.Failure("Billing subtotal cannot be negative");

        if (discount.GetValueOrDefault() < 0)
            return BusinessRuleResult.Failure("Billing discount cannot be negative");

        if (insuranceCoverage.GetValueOrDefault() < 0)
            return BusinessRuleResult.Failure("Insurance coverage cannot be negative");

        var total = CalculateTotal(subtotal, discount, insuranceCoverage);

        return total < 0
            ? BusinessRuleResult.Failure("Billing discounts and coverage cannot exceed subtotal")
            : BusinessRuleResult.Success();
    }

    public decimal CalculateTotal(
        decimal subtotal,
        decimal? discount,
        decimal? insuranceCoverage)
    {
        return subtotal - discount.GetValueOrDefault() - insuranceCoverage.GetValueOrDefault();
    }
}
